namespace FamilyTree.Web
{
    public class PersonViewModel
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Autobiography { get; set; }
        public string? Father { get; set; }
        public string? Mother { get; set; }
        private List<PersonViewModel> children = new List<PersonViewModel>();
        public List<PersonViewModel> Children { get => children; set { value = children; } }
        private List<PersonViewModel> together = new List<PersonViewModel>();
        public List<PersonViewModel> Together { get => together; set { value = together; } }
        public string? ImagePath { get; set; }
        public string? Husband { get; set; }
        public string? Wife { get; set; }
        public DateTime? MariedSince { get; set; }
        public string? Address { get; set; }
        public string? Color { get; set; }
        public string Generation { get; set; }
        int Wifecount = 0;
        public int wifecount { get => Wifecount; set { Wifecount = value; } }


        public PersonViewModel DeepCopy()
        {
            PersonViewModel copy = new PersonViewModel
            {
                Name = this.Name,
                Gender = this.Gender,
                DateOfBirth = this.DateOfBirth,
                Autobiography = this.Autobiography,
                Father = this.Father,
                Mother = this.Mother,
                ImagePath = this.ImagePath,
                Husband = this.Husband,
                Wife = this.Wife,
                MariedSince = this.MariedSince,
                Address = this.Address,
                Generation = this.Generation,
                Wifecount = this.Wifecount
            };

            // Copy Children list
            foreach (var child in this.Children)
            {
                copy.Children.Add(child.DeepCopy());
            }

            // Copy Together list
            foreach (var partner in this.Together)
            {
                copy.Together.Add(partner.DeepCopy());
            }

            return copy;
        }
    }

}
