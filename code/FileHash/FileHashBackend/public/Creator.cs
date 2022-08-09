using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHashBackend
{
    public class Creator
    {
        private static readonly Lazy<Creator> _instance =
                new Lazy<Creator>(() => new Creator());

        public static Creator Instance
        {
            get { return _instance.Value; }
        }

        public IHasher GetHasher(HasherType type)
        {
            return new Impl.Hasher(type);
        }

        public IFinder GetFinder(HasherType type)
        {
            return new Impl.Finder(new Impl.Hasher(type));
        }
        private Creator(){}
    }
}
