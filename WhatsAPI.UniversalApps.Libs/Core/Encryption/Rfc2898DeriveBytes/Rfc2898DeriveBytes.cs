﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAPI.UniversalApps.Libs.Core.Encryption.Rfc2898DeriveBytes
{
    [ComVisible(true)]
    public class Rfc2898DeriveBytes : DeriveBytes
    {

        private const int defaultIterations = 1000;

        private int _iteration;
        private byte[] _salt;
        private HMACSHA1.HMACSHA1 _hmac;
        private byte[] _buffer;
        private int _pos;
        private int _f;

        // constructors

        public Rfc2898DeriveBytes(string password, byte[] salt)
            : this(password, salt, defaultIterations)
        {
        }

        public Rfc2898DeriveBytes(string password, byte[] salt, int iterations)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            Salt = salt;
            IterationCount = iterations;
            _hmac = new HMACSHA1.HMACSHA1(Encoding.UTF8.GetBytes(password));
        }

        public Rfc2898DeriveBytes(byte[] password, byte[] salt, int iterations)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            Salt = salt;
            IterationCount = iterations;
            _hmac = new HMACSHA1.HMACSHA1(password);
        }

        public Rfc2898DeriveBytes(string password, int saltSize)
            : this(password, saltSize, defaultIterations)
        {
        }

        public Rfc2898DeriveBytes(string password, int saltSize, int iterations)
        {
            if (password == null)
                throw new ArgumentNullException("password");
            if (saltSize < 0)
                throw new ArgumentOutOfRangeException("invalid salt length");

            Salt = WhatsAPI.UniversalApps.Libs.Core.Encryption.CryptoTools.KeyBuilder.Key(saltSize);
            IterationCount = iterations;
            _hmac = new HMACSHA1.HMACSHA1(Encoding.UTF8.GetBytes(password));
        }

        // properties

        public int IterationCount
        {
            get { return _iteration; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("IterationCount < 1");

                _iteration = value;
            }
        }

        public byte[] Salt
        {
            get { return (byte[])_salt.Clone(); }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Salt");
                if (value.Length < 8)
                    throw new ArgumentException("Salt < 8 bytes");

                _salt = (byte[])value.Clone();
            }
        }

        // methods

        private byte[] F(byte[] s, int c, int i)
        {
            s[s.Length - 4] = (byte)(i >> 24);
            s[s.Length - 3] = (byte)(i >> 16);
            s[s.Length - 2] = (byte)(i >> 8);
            s[s.Length - 1] = (byte)i;

            // this is like j=0
            byte[] u1 = _hmac.ComputeHash(s);
            byte[] data = u1;
            // so we start at j=1
            for (int j = 1; j < c; j++)
            {
                byte[] un = _hmac.ComputeHash(data);
                // xor
                for (int k = 0; k < 20; k++)
                    u1[k] = (byte)(u1[k] ^ un[k]);
                data = un;
            }
            return u1;
        }

        public override byte[] GetBytes(int cb)
        {
            if (cb < 1)
                throw new ArgumentOutOfRangeException("cb");

            int l = cb / 20;	// HMACSHA1 == 160 bits == 20 bytes
            int r = cb % 20;	// remainder
            if (r != 0)
                l++;		// rounding up

            byte[] result = new byte[cb];
            int rpos = 0;
            if (_pos > 0)
            {
                int count = Math.Min(20 - _pos, cb);
                Buffer.BlockCopy(_buffer, _pos, result, 0, count);
                if (count >= cb)
                    return result;

                // If we are going to go over the boundaries
                // of the result on our l-1 iteration, reduce
                // l to compensate.
                if (((l - 1) * 20 + count) > result.Length)
                    l--;

                _pos = 0;
                rpos = count;
            }

            byte[] data = new byte[_salt.Length + 4];
            Buffer.BlockCopy(_salt, 0, data, 0, _salt.Length);

            for (int i = 1; i <= l; i++)
            {
                _buffer = F(data, _iteration, ++_f);
                // we may not need the complete last block
                int count = ((i == l) ? result.Length - rpos : 20);
                Buffer.BlockCopy(_buffer, _pos, result, rpos, count);
                rpos += _pos + count;
                _pos = ((count == 20) ? 0 : count);
            }

            return result;
        }

        public override void Reset()
        {
            _buffer = null;
            _pos = 0;
            _f = 0;
        }
#if NET_4_0
		protected override void Dispose (bool disposing)
		{
			Array.Clear (_buffer, 0, _buffer.Length);
			Array.Clear (_salt, 0, _salt.Length);
			_hmac.Clear ();
			base.Dispose (disposing);
		}
#endif
    } 
}
