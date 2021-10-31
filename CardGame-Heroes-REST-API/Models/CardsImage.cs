using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CardGame_Heroes_REST_API.Models
{
    /// <summary>
    /// Таблица внешних ссылок на изображения
    /// </summary>
    public partial class CardsImage
    {
        [JsonIgnore]
        public ulong CardId { get; set; }
        /// <summary>
        /// Название карты
        /// </summary>
        [JsonIgnore]
        public string CardName { get; set; } = null!;
        /// <summary>
        /// Внешняя ссылка на целое изображение карты
        /// </summary>
        public string? CardImageUrl { get; set; }
        /// <summary>
        /// Внешняя ссылка на изображение персонажа внутри карты
        /// </summary>
        public string? ArtworkUrl { get; set; }

        [JsonIgnore]
        public virtual Card Card { get; set; } = null!;
    }
}
