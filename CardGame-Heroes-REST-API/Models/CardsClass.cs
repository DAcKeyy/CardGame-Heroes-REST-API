using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CardGame_Heroes_REST_API.Models
{
    /// <summary>
    /// Таблица классов карт
    /// </summary>
    public partial class CardsClass
    {
        public CardsClass()
        {
            Cards = new HashSet<Card>();
        }

        /// <summary>
        /// Название класса
        /// </summary>
        public string ClassName { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Card> Cards { get; set; }
    }
}
