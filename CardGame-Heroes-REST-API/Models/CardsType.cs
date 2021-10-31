using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CardGame_Heroes_REST_API.Models
{
    /// <summary>
    /// Таблица типов карт
    /// </summary>
    public partial class CardsType
    {
        public CardsType()
        {
            Cards = new HashSet<Card>();
        }

        /// <summary>
        /// Название типа карты
        /// </summary>
        public string TypeName { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Card> Cards { get; set; }
    }
}
