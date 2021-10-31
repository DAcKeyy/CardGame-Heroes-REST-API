using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CardGame_Heroes_REST_API.Models
{
    /// <summary>
    /// Таблица стихий карт
    /// </summary>
    public partial class CardsElement
    {
        public CardsElement()
        {
            Cards = new HashSet<Card>();
        }

        /// <summary>
        /// Название стихии
        /// </summary>
        public string ElementName { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Card> Cards { get; set; }
    }
}
