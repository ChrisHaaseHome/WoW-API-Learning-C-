using System;

namespace MountList
{

    public class Mount : IEquatable<Mount> , IComparable
    {
        int IComparable.CompareTo(object other)
        {
            Mount y = (Mount)other;
            return String.Compare(this.name, y.name);
        }

        public override bool Equals(object obj)
        {
            Mount y;

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            y = (Mount)obj;
            return this.name.Equals(y.name);
        }

        public bool Equals(Mount other)
        {
            if (other == null)
                return false;

            return this.name.Equals(other.name);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            int hashCode;

            hashCode = name.GetHashCode() ^ spellId.GetHashCode() ^ creatureId.GetHashCode() ^ itemId.GetHashCode();
            hashCode ^= qualityId.GetHashCode() ^ isGround.GetHashCode() ^ isFlying.GetHashCode();
            hashCode ^= isAquatic.GetHashCode() ^ isJumping.GetHashCode();
            if (icon != null)
                hashCode ^= icon.GetHashCode();

            return hashCode;
        }


        public string name { get; set; }
        public int spellId { get; set; }
        public int creatureId { get; set; }
        public int itemId { get; set; }
        public int qualityId { get; set; }
        public string icon { get; set; }
        public bool isGround { get; set; }
        public bool isFlying { get; set; }
        public bool isAquatic { get; set; }
        public bool isJumping { get; set; }
    }

}
