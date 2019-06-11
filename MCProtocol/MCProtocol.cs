using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public class MCProtocol
    {
        public CommunicationDataCode DataCode { get; set; } = CommunicationDataCode.None;

        public MCProtocol()
        {

        }

        public MCQHeader CreateQHeader(PacketDirection direction)
        {
            if (direction == PacketDirection.Request)
            {
                return new MCQHeaderRequest();
            }
            else if (direction == PacketDirection.Response)
            {
                return new MCQHeaderResponse();
            }

            return null;
        }

        public MCDataProcessor CreateDataProcessor(Stream stream)
        {
            var dataCode = this.DataCode;

            if (dataCode == CommunicationDataCode.BINARY)
            {
                return new MCDataProcessorBinary(stream) { LittleEndian = true };
            }
            else if (dataCode == CommunicationDataCode.ASCII)
            {
                return new MCDataProcessorAscii(stream) { LittleEndian = false };
            }

            return null;
        }

        public MCPacket ReadPacket(Stream stream, MCFunction requestFunction)
        {
            var packet = new MCPacket();

            var source = this.CreateDataProcessor(stream);
            source.ForceBigEndian = true;
            var frameId = source.ReadUShort();
            source.ForceBigEndian = false;
            var frame = MCFrame.Find(frameId);
            var direction = frame.GetDirection(frameId);

            var subHeader = MCSubHeader.Find(frame).CreateInstance();
            subHeader.Read(source);
            packet.SubHeader = subHeader;

            var qHeader = this.CreateQHeader(direction);
            qHeader.ReadPre(source);
            packet.QHeader = qHeader;

            ushort dataLength = (ushort)(source.ReadUShort() / source.LengthPerByte);
            var data = source.ReadBytes(dataLength);

            using (var ms = new MemoryStream(data))
            {
                var dataSource = this.CreateDataProcessor(ms);
                dataSource.IgnoreConvert = true;
                qHeader.ReadPost(dataSource);

                var function = this.CreateFucntion(requestFunction, direction, qHeader, dataSource);

                if (function != null)
                {
                    function.Read(dataSource);
                }

                packet.Function = function;
            }

            return packet;
        }

        private MCFunction CreateFucntion(MCFunction requestFunction, PacketDirection direction, MCQHeader qHeader, MCDataProcessor dataSource)
        {
            if (direction == PacketDirection.Request)
            {
                var cc = dataSource.ReadUShort();
                var sc = dataSource.ReadUShort();
                var functionRegistration = MCFunctionRegistry.Find(cc);

                if (functionRegistration != null)
                {
                    return functionRegistration.Generate(direction, sc, requestFunction);
                }

            }
            else if (direction == PacketDirection.Response)
            {
                var qHeaderResponse = (MCQHeaderResponse)qHeader;

                if (qHeaderResponse.ResultCode == 0)
                {
                    var functionRegistration = MCFunctionRegistry.Find(requestFunction.GetType());

                    if (functionRegistration != null)
                    {
                        return functionRegistration.Generate(direction, requestFunction.GetSubCommandCode(), requestFunction);
                    }

                }
                else
                {
                    return new MCFunctionErrorResponse();
                }

            }

            return null;
        }

        public void WritePacket(Stream stream, MCPacket packet)
        {
            var direction = packet.Function.GetDirection();
            var target = this.CreateDataProcessor(stream);

            var frame = MCSubHeader.Find(packet.SubHeader.GetType()).Frame;
            var frameId = frame.GetId(direction);
            target.ForceBigEndian = true;
            target.WriteUShort(frameId);
            target.ForceBigEndian = false;

            var subHeader = packet.SubHeader;
            subHeader.Write(target);

            var qHeader = packet.QHeader;
            qHeader.WritePre(target);

            using (var ms = new MemoryStream())
            {
                var dataTarget = this.CreateDataProcessor(ms);
                qHeader.WritePost(dataTarget);
                var function = packet.Function;

                if (direction == PacketDirection.Request)
                {
                    dataTarget.WriteUShort(MCFunctionRegistry.Find(function.GetType()).Id);
                    dataTarget.WriteUShort(function.GetSubCommandCode());
                }

                function.Write(dataTarget);

                var data = ms.ToArray();
                target.WriteUShort((ushort)data.Length);
                target.IgnoreConvert = true;
                target.WriteBytes(data);
            }

        }

    }

}
