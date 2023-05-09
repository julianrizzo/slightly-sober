using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace slightly_sober.Models
{
    public class Cocktail
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CocktailID { get; set; }

        [Required, StringLength(30)]
        public string Name { get; set; }

        [Required, StringLength(30)]
        public string Description { get; set; }

        [Required, StringLength(30)]
        public string Type { get; set; }

        [Required, StringLength(30)]
        public string MainLiquer { get; set; }

        [Required, StringLength(200)]
        public string Ingredients { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User User { get; set; }

        [SetsRequiredMembers]
        public Cocktail(string name, string description, string type, string mainLiquer, string ingredients, int userID)
        {
            Name = name;
            Description = description;
            Type = type;
            MainLiquer = mainLiquer;
            Ingredients = ingredients;
            UserID = userID;
        }
    }
}
