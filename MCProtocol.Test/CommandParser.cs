using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public static class CommandParser
    {
        public static Tuple<ushort, ushort> ParseDevices(string devices, out DeviceCode code)
        {
            int len = ParseDeviceCode(devices, out code);

            if (len == -1)
            {
                return null;
            }

            devices = devices.Substring(len);
            var splited = devices.Split(new string[] { ".." }, StringSplitOptions.None);
            ushort offset = 0;
            ushort count = 0;

            if (splited.Length == 2)
            {
                var offsetToString = splited[0];
                var countToString = splited[1];

                if (ushort.TryParse(offsetToString, out offset) == false || ushort.TryParse(countToString, out count) == false)
                {
                    return null;
                }

            }
            else if (splited.Length == 1)
            {
                var offsetToString = splited[0];

                if (ushort.TryParse(offsetToString, out offset) == false)
                {
                    return null;
                }

                count = 1;
            }
            else
            {
                return null;
            }

            return new Tuple<ushort, ushort>(offset, count);
        }

        public static int ParseDeviceCode(string text, out DeviceCode code)
        {
            text = text.ToUpperInvariant();

            var codes = (DeviceCode[])Enum.GetValues(typeof(DeviceCode));
            DeviceCode find = DeviceCode.None;
            int findLength = -1;

            foreach (var c in codes)
            {
                var toString = c.ToString().ToUpperInvariant();

                if (text.StartsWith(toString) == true)
                {
                    var length = toString.Length;

                    if (length > findLength)
                    {
                        findLength = length;
                        find = c;
                    }

                }

            }

            code = find;
            return findLength;
        }

        public static ushort[] ParseValues(string text)
        {
            var splited = text.Split(',');
            ushort[] values = new ushort[splited.Length];

            for (int i = 0; i < values.Length; i++)
            {
                ushort value = 0;

                if (ushort.TryParse(splited[i], out value) == false)
                {
                    return null;
                }

                values[i] = value;
            }

            return values;
        }

        public static Command Parse(string commandLine)
        {
            var splited = commandLine.Split('=');
            string devicesToString = null;
            string valuesToString = null;

            if (splited.Length == 2)
            {
                devicesToString = splited[0];
                valuesToString = splited[1];
            }
            else if (splited.Length == 1)
            {
                devicesToString = splited[0];
            }
            else
            {
                return null;
            }

            DeviceCode code = DeviceCode.None;
            var devices = ParseDevices(devicesToString, out code);

            if (devices == null)
            {
                return null;
            }

            if (valuesToString == null)
            {
                var command = new CommandGet();
                command.DeviceCode = code;
                command.Offset = devices.Item1;
                command.Count = devices.Item2;

                return command;
            }
            else
            {
                int count = devices.Item2;
                var values = ParseValues(valuesToString);

                if (values.Length != count)
                {
                    return null;
                }

                var command = new CommandSet();
                command.DeviceCode = code;
                command.Offset = devices.Item1;
                command.Values = values;

                return command;
            }

        }

    }

}
