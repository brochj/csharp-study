using Oop.ContentContext.Enums;

namespace Oop.ContentContext
{
    public class Lecture : Base
    {
        public int Order { get; set; }
        public int Title { get; set; }
        public int DurationInMinutes { get; set; }

        public EContentLevel Level { get; set; }

    }
}