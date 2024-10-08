using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdateTest
{
    internal class VersionNumber : IComparable<VersionNumber>
    {
        public VersionNumber(int v1, int v2, int v3)
        {
            major = v1;
            minor = v2;
            build = v3;
        }

        public VersionNumber(string tagName) : this(
            int.Parse(tagName.Split('.')[0]),
            int.Parse(tagName.Split('.')[1]),
            int.Parse(tagName.Split('.')[2]))
        {
        }

        public override string ToString()
        {
            return $"{major}.{minor}.{build}";
        }

        public int CompareTo(VersionNumber other)
        {
            if (other == null) return 1;

            if (major != other.major)
                return major.CompareTo(other.major);
            if (minor != other.minor)
                return minor.CompareTo(other.minor);
            return build.CompareTo(other.build);
        }

        public static bool operator >(VersionNumber v1, VersionNumber v2)
        {
            return v1.CompareTo(v2) > 0;
        }

        public static bool operator <(VersionNumber v1, VersionNumber v2)
        {
            return v1.CompareTo(v2) < 0;
        }

        public int major { get; }
        public int minor { get; }
        public int build { get; }
    }
}
