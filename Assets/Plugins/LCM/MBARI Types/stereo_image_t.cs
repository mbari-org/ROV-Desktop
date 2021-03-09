/* LCM type definition class file
 * This file was automatically generated by lcm-gen
 * DO NOT MODIFY BY HAND!!!!
 */

using System;
using System.Collections.Generic;
using System.IO;
using LCM.LCM;
 
namespace mwt
{
    public sealed class stereo_image_t : LCM.LCM.LCMEncodable
    {
        public String publisher;
        public double cam1_timestamp;
        public double cam2_timestamp;
        public long left_utime;
        public long right_utime;
        public long sequence;
        public int camera_orientation;
        public int width;
        public int height;
        public int row_stride;
        public int pixelformat;
        public int size;
        public byte[] data;
 
        public stereo_image_t()
        {
        }
 
        public static readonly ulong LCM_FINGERPRINT;
        public static readonly ulong LCM_FINGERPRINT_BASE = 0xfd5186f91b301327L;
 
        public const int CAM_HORIZONTAL = 1;
        public const int CAM_VERTICAL = 2;
        public const int PIXEL_FORMAT_UYVY = 1498831189;
        public const int PIXEL_FORMAT_YUYV = 1448695129;
        public const int PIXEL_FORMAT_IYU1 = 827677001;
        public const int PIXEL_FORMAT_IYU2 = 844454217;
        public const int PIXEL_FORMAT_YUV420 = 842093913;
        public const int PIXEL_FORMAT_YUV411P = 1345401140;
        public const int PIXEL_FORMAT_I420 = 808596553;
        public const int PIXEL_FORMAT_NV12 = 842094158;
        public const int PIXEL_FORMAT_GRAY = 1497715271;
        public const int PIXEL_FORMAT_RGB = 859981650;
        public const int PIXEL_FORMAT_BGR = 861030210;
        public const int PIXEL_FORMAT_RGBA = 876758866;
        public const int PIXEL_FORMAT_BGRA = 877807426;
        public const int PIXEL_FORMAT_BAYER_BGGR = 825770306;
        public const int PIXEL_FORMAT_BAYER_GBRG = 844650584;
        public const int PIXEL_FORMAT_BAYER_GRBG = 861427800;
        public const int PIXEL_FORMAT_BAYER_RGGB = 878205016;
        public const int PIXEL_FORMAT_BE_BAYER16_BGGR = 826360386;
        public const int PIXEL_FORMAT_BE_BAYER16_GBRG = 843137602;
        public const int PIXEL_FORMAT_BE_BAYER16_GRBG = 859914818;
        public const int PIXEL_FORMAT_BE_BAYER16_RGGB = 876692034;
        public const int PIXEL_FORMAT_LE_BAYER16_BGGR = 826360396;
        public const int PIXEL_FORMAT_LE_BAYER16_GBRG = 843137612;
        public const int PIXEL_FORMAT_LE_BAYER16_GRBG = 859914828;
        public const int PIXEL_FORMAT_LE_BAYER16_RGGB = 876692044;
        public const int PIXEL_FORMAT_MJPEG = 1196444237;
        public const int PIXEL_FORMAT_BE_GRAY16 = 357;
        public const int PIXEL_FORMAT_LE_GRAY16 = 909199180;
        public const int PIXEL_FORMAT_BE_RGB16 = 358;
        public const int PIXEL_FORMAT_LE_RGB16 = 1279412050;
        public const int PIXEL_FORMAT_BE_SIGNED_GRAY16 = 359;
        public const int PIXEL_FORMAT_BE_SIGNED_RGB16 = 360;
        public const int PIXEL_FORMAT_FLOAT_GRAY32 = 842221382;
        public const int PIXEL_FORMAT_INVALID = -2;
        public const int PIXEL_FORMAT_ANY = -1;

        static stereo_image_t()
        {
            LCM_FINGERPRINT = _hashRecursive(new List<String>());
        }
 
        public static ulong _hashRecursive(List<String> classes)
        {
            if (classes.Contains("mwt.stereo_image_t"))
                return 0L;
 
            classes.Add("mwt.stereo_image_t");
            ulong hash = LCM_FINGERPRINT_BASE
                ;
            classes.RemoveAt(classes.Count - 1);
            return (hash<<1) + ((hash>>63)&1);
        }
 
        public void Encode(LCMDataOutputStream outs)
        {
            outs.Write((long) LCM_FINGERPRINT);
            _encodeRecursive(outs);
        }
 
        public void _encodeRecursive(LCMDataOutputStream outs)
        {
            byte[] __strbuf = null;
            __strbuf = System.Text.Encoding.GetEncoding("US-ASCII").GetBytes(this.publisher); outs.Write(__strbuf.Length+1); outs.Write(__strbuf, 0, __strbuf.Length); outs.Write((byte) 0); 
 
            outs.Write(this.cam1_timestamp); 
 
            outs.Write(this.cam2_timestamp); 
 
            outs.Write(this.left_utime); 
 
            outs.Write(this.right_utime); 
 
            outs.Write(this.sequence); 
 
            outs.Write(this.camera_orientation); 
 
            outs.Write(this.width); 
 
            outs.Write(this.height); 
 
            outs.Write(this.row_stride); 
 
            outs.Write(this.pixelformat); 
 
            outs.Write(this.size); 
 
            for (int a = 0; a < this.size; a++) {
                outs.Write(this.data[a]); 
            }
 
        }
 
        public stereo_image_t(byte[] data) : this(new LCMDataInputStream(data))
        {
        }
 
        public stereo_image_t(LCMDataInputStream ins)
        {
            if ((ulong) ins.ReadInt64() != LCM_FINGERPRINT)
                throw new System.IO.IOException("LCM Decode error: bad fingerprint");
 
            _decodeRecursive(ins);
        }
 
        public static mwt.stereo_image_t _decodeRecursiveFactory(LCMDataInputStream ins)
        {
            mwt.stereo_image_t o = new mwt.stereo_image_t();
            o._decodeRecursive(ins);
            return o;
        }
 
        public void _decodeRecursive(LCMDataInputStream ins)
        {
            byte[] __strbuf = null;
            __strbuf = new byte[ins.ReadInt32()-1]; ins.ReadFully(__strbuf); ins.ReadByte(); this.publisher = System.Text.Encoding.GetEncoding("US-ASCII").GetString(__strbuf);
 
            this.cam1_timestamp = ins.ReadDouble();
 
            this.cam2_timestamp = ins.ReadDouble();
 
            this.left_utime = ins.ReadInt64();
 
            this.right_utime = ins.ReadInt64();
 
            this.sequence = ins.ReadInt64();
 
            this.camera_orientation = ins.ReadInt32();
 
            this.width = ins.ReadInt32();
 
            this.height = ins.ReadInt32();
 
            this.row_stride = ins.ReadInt32();
 
            this.pixelformat = ins.ReadInt32();
 
            this.size = ins.ReadInt32();
 
            this.data = new byte[(int) size];
            for (int a = 0; a < this.size; a++) {
                this.data[a] = ins.ReadByte();
            }
 
        }
 
        public mwt.stereo_image_t Copy()
        {
            mwt.stereo_image_t outobj = new mwt.stereo_image_t();
            outobj.publisher = this.publisher;
 
            outobj.cam1_timestamp = this.cam1_timestamp;
 
            outobj.cam2_timestamp = this.cam2_timestamp;
 
            outobj.left_utime = this.left_utime;
 
            outobj.right_utime = this.right_utime;
 
            outobj.sequence = this.sequence;
 
            outobj.camera_orientation = this.camera_orientation;
 
            outobj.width = this.width;
 
            outobj.height = this.height;
 
            outobj.row_stride = this.row_stride;
 
            outobj.pixelformat = this.pixelformat;
 
            outobj.size = this.size;
 
            outobj.data = new byte[(int) size];
            for (int a = 0; a < this.size; a++) {
                outobj.data[a] = this.data[a];
            }
 
            return outobj;
        }
    }
}
