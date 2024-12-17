namespace RandomNameGenerator.Models
{
    public class ClanNameGenerator
    {
        private readonly List<string> _prefixes = new List<string>
        {
            "Dark", "Storm", "Blood", "Iron", "Shadow", "Fire", "Ice",
            "Thunder", "Night", "Dawn", "Dusk", "Wolf", "Dragon", "Star"
        };

        private readonly List<string> _suffixes = new List<string>
        {
            "blade", "heart", "claw", "fang", "wing", "fury", "guard",
            "shield", "soul", "spirit", "hunter", "walker", "bringer", "seeker"
        };

        private readonly Random _random = new Random();

        public string GenerateClanN()
        {
            var _prefix = _prefixes[_random.Next(_prefixes.Count)];
            var _suffix = _suffixes[_random.Next(_prefixes.Count)];
            return $"{_prefix}{_suffix}";
        }
    }
}
