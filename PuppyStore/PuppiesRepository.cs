using System.Collections.Generic;
using System.Linq;

namespace PuppyStore
{
    public class PuppiesRepository
    {
        public class Puppy
        {
            public int NumericId { get; set; }
            public string Id { get; set; } = "";
            public string Name { get; set; } = "";
            public string Breed { get; set; } = "";
            public string Sex { get; set; } = "";
            public string ImageUrl { get; set; } = "";
            public decimal Price { get; set; }
            public string Description { get; set; } = "";
        }

private List<Puppy> puppies = new()
{
    new Puppy { Id="bella", NumericId = 1, Name="Bella", Breed="Maltipoo", Sex="Female", ImageUrl="puppy1.jpg", Price=1299,
        Description="Bella is an affectionate little Maltipoo with a calm, gentle spirit. She loves curling up beside you during quiet evenings and will happily nap on your lap for hours. Bella is wonderful with children and thrives in a warm, loving home where she gets plenty of cuddles, soft blankets, and cozy spots to relax. Her small size and sweet temperament make her the perfect companion for families, apartment living, or anyone looking for a soft-hearted, easygoing puppy." },

    new Puppy { Id="charlie", NumericId = 2, Name="Charlie", Breed="Golden Retriever", Sex="Male", ImageUrl="puppy2.jpg", Price=1499,
        Description="Charlie is the picture-perfect Golden Retriever—friendly, outgoing, and endlessly loyal. He loves exploring the outdoors, playing fetch, and greeting everyone he meets with a wagging tail. Charlie is wonderful with children, incredibly patient, and always eager to learn. Whether you’re looking for a hiking buddy or a snuggly best friend, Charlie’s cheerful kindness and social personality make him a perfect match for active families." },

    new Puppy { Id="daisy", NumericId = 3, Name="Daisy", Breed="Pomeranian", Sex="Female", ImageUrl="puppy3.jpg", Price=1399,
        Description="Daisy is a bright, playful Pomeranian full of personality and joy. She adores being the center of attention and will happily follow you from room to room just to be near you. Daisy is energetic, curious, and loves playing with toys or exploring the backyard. Despite her tiny size, she has a bold and loving heart, making her a wonderful match for families who want a lively, affectionate little companion." },

    new Puppy { Id="milo", NumericId = 4, Name="Milo", Breed="Golden Retriever", Sex="Male", ImageUrl="puppy4.jpg", Price=1499,
        Description="Milo is a high-energy Golden Retriever who thrives on adventure. He loves outdoor activities like running, hiking, swimming, and playing fetch until he drops. Milo’s enthusiasm is contagious, and he brings a bright, playful energy into every home he enters. Even with all his energy, he is incredibly loving and gentle, always ready to cuddle after a fun day outside. He is perfect for families who enjoy an active lifestyle and want a loyal dog to join in the fun." },

    new Puppy { Id="luna", NumericId = 5, Name="Luna", Breed="Maltipoo", Sex="Female", ImageUrl="puppy5.jpg", Price=1299,
        Description="Luna is a calm, gentle Maltipoo who loves slow mornings, soft blankets, and warm company. She is affectionate without being overwhelming and prefers peaceful environments where she can relax beside her favorite people. Luna enjoys simple playtime but is happiest when she can curl up on your lap or nap nearby. She is wonderfully patient, making her a great match for first-time dog owners or families looking for a sweet, low-energy companion." },

    new Puppy { Id="hunter", NumericId = 6, Name="Hunter", Breed="English Lab", Sex="Male", ImageUrl="EnglishLab5.jpeg", Price=1000,
        Description="Hunter is a classic English Lab—gentle, steady, and incredibly devoted. He has a calm, thoughtful personality and loves being close to his people, especially during quiet evenings or family time. Hunter enjoys leisurely walks, playing with children, and learning new commands. His balanced temperament makes him an excellent family dog, especially for households that appreciate the Lab’s loyal, affectionate nature." },

    new Puppy { Id="winston", NumericId = 7, Name="Winston", Breed="English Lab", Sex="Male", ImageUrl="EnglishLab2.jpg", Price=1000,
        Description="Winston is a happy, well-mannered English Lab who thrives on companionship and structure. He loves exploring outdoors, meeting new people, and showing off how quickly he can learn new tricks. Winston has a warm, gentle heart and forms strong bonds with his family. He is ideal for homes looking for a dependable, loving dog who brings both joy and calmness into everyday life." },

    new Puppy { Id="penelope", NumericId = 8, Name="Penelope", Breed="Goldendoodle", Sex="Female", ImageUrl="goldendoodle1.jpg", Price=1500,
        Description="Penelope is a charming Goldendoodle with a playful spirit and a brilliant mind. She loves interacting with people, learning new commands, and zooming around the yard with excitement. Penelope is affectionate and loyal, making her a wonderful companion for families, children, or anyone looking for a smart, energetic puppy. Her fluffy coat and cheerful personality make her especially irresistible." },

    new Puppy { Id="ollie", NumericId = 9, Name="Ollie", Breed="Goldendoodle", Sex="Male", ImageUrl="goldendoodle3.jpg", Price=1499,
        Description="Ollie is a spirited and fun-loving Goldendoodle who brings excitement into every room he enters. He enjoys running, playing tug-of-war, and discovering new toys. Ollie’s intelligence and eagerness to learn make him easy to train, while his affectionate side makes him a wonderful snuggle buddy after a day of play. He is perfect for families who want an energetic and engaging puppy with a big heart." },

    new Puppy { Id="rigby", NumericId = 10, Name="Rigby", Breed="Goldendoodle", Sex="Male", ImageUrl="goldendoodle4.jpg", Price=1300,
        Description="Rigby is a gentle, soft-hearted Goldendoodle who loves companionship and routine. He is sweet, calm, and always eager to please. Rigby enjoys going on walks, relaxing beside his family, and being included in daily activities. His mellow personality makes him a great fit for families, couples, or individuals looking for a loving companion who brings warmth and joy into everyday life." },
};


        public IEnumerable<Puppy> All() => puppies;
        public Puppy? FindById(string id) => puppies.FirstOrDefault(p => p.Id == id);
    }
}

