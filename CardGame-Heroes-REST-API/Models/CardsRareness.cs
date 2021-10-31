using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CardGame_Heroes_REST_API.Models
{
    /// <summary>
    /// Таблица редкости карт
    /// </summary>
    public partial class CardsRareness
    {
        public CardsRareness()
        {
            Cards = new HashSet<Card>();
        }

        /// <summary>
        /// Назавание редкости карты
        /// </summary>
        public string RarenessName { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Card> Cards { get; set; }
    }
}
