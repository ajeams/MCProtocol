using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public class MCFrame
    {
        private static Dictionary<ushort, MCFrame> Caches { get; } = new Dictionary<ushort, MCFrame>();

        public static MCFrame Find(ushort id)
        {
            return Caches.TryGetValue(id, out var value) ? value : null;
        }

        public static MCFrame[] Values
        {
            get
            {
                return Caches.Values.ToArray();
            }

        }

        public static MCFrame MC3E { get; } = new MCFrame(0x5000, 0xD000);
        public static MCFrame MC4E { get; } = new MCFrame(0x5400, 0xD400);

        public ushort RequestId { get; }
        public ushort ResponseId { get; }


        private MCFrame(ushort requestId, ushort responseId)
        {
            this.RequestId = requestId;
            this.ResponseId = responseId;

            Caches[requestId] = this;
            Caches[responseId] = this;
        }

        public ushort GetId(PacketDirection direction)
        {
            if (direction == PacketDirection.Request)
            {
                return this.RequestId;
            }
            else if (direction == PacketDirection.Response)
            {
                return this.ResponseId;
            }

            return 0;
        }

        public PacketDirection GetDirection(ushort id)
        {
            if (id == this.RequestId)
            {
                return PacketDirection.Request;
            }
            else if (id == this.ResponseId)
            {
                return PacketDirection.Response;
            }

            return PacketDirection.None;
        }

    }

}
