﻿using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace SolCodeGen.AbiEncoding.Encoders
{
    public class AddressEncoder : AbiTypeEncoder<Address>
    {
        static readonly byte[] ZEROx12 = Enumerable.Repeat((byte)0, 12).ToArray();

        // encoded the same way as an uint160
        public override void Encode(ref AbiEncodeBuffer buffer)
        {
            MemoryMarshal.Write(buffer.HeadCursor.Slice(12), ref _val);
            buffer.IncrementHeadCursor(UInt256.SIZE);
        }

        public override void Decode(ref AbiDecodeBuffer buff, out Address val)
        {
            // data validity check: 20 address bytes should be left-padded with 12 zero-bytes
            // Disabled - ganache liters this padding with garbage bytes
            /*
            if (!buffer.Slice(0, 12).SequenceEqual(ZEROx12))
            {
                throw new ArgumentException("Invalid address input data; should be 20 address bytes, left-padded with 12 zero-bytes; received: " + buffer.Slice(0, UInt256.SIZE).ToHexString());
            }
            */
            val = MemoryMarshal.Read<Address>(buff.HeadCursor.Slice(12));
            buff.IncrementHeadCursor(UInt256.SIZE);
        }
    }

}
