using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CardGame_Heroes_REST_API.Models
{
    /// <summary>
    /// Таблица выпусков (игровых наборов) карт
    /// </summary>
    public partial class CardsSet
    {
        public CardsSet()
        {
            Cards = new HashSet<Card>();
        }

        /// <summary>
        /// Название выпуска
        /// </summary>
        public string SetName { get; set; } = null!;
        /// <summary>
        /// Колличество карт в выпуске
        /// </summary>
        public short CardsAmount { get; set; }

        [JsonIgnore]
        public virtual ICollection<Card> Cards { get; set; }
    }
}
