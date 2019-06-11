﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public enum DeviceCode : byte
    {
        None = 0x00,

        SM = 0x91,
        SD = 0xA9,
        X = 0x9C,
        Y = 0x9D,
        M = 0x90,
        L = 0x92,
        F = 0x93,
        V = 0x94,
        B = 0xA0,
        D = 0xA8,
        W = 0xB4,
        TS = 0xC1,
        TC = 0xC0,
        TN = 0xC2,
        SS = 0xC7,
        SC = 0xC6,
        SN = 0xC8,
        CS = 0xC4,
        CC = 0xC3,
        CN = 0xC5,
        SB = 0xA1,
        SW = 0xB5,
        S = 0x98,
        DX = 0xA2,
        DY = 0xA3,
        Z = 0xCC,
        R = 0xAF,
        ZR = 0xB0,

    }

}
