using System;
using System.Collections.Generic;

namespace CardGame_Heroes_REST_API.Models
{
    public partial class Card
    {
        /// <summary>
        /// Инкрементное значения карты в базе
        /// </summary>
        public ulong Id { get; set; }
        /// <summary>
        /// Название карты
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Название типа карты
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// Название стихии
        /// </summary>
        public string? Element { get; set; }
        /// <summary>
        /// Называние редкости
        /// </summary>
        public string? Rareness { get; set; }
        /// <summary>
        /// Название класса
        /// </summary>
        public string? Class { get; set; }
        /// <summary>
        /// Функциональное описание карты
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Сюжетное описание карты, не влияющиее на игровой процесс
        /// </summary>
        public string? FlavorText { get; set; }
        /// <summary>
        /// Название выпуска
        /// </summary>
        public string? SetName { get; set; }
        /// <summary>
        /// Стоимость карты в монетах
        /// (-1 если Х)
        /// </summary>
        public short? Cost { get; set; }
        /// <summary>
        /// Показатель здоровья карты (-1 если Х)
        /// </summary>
        public short? Health { get; set; }
        /// <summary>
        /// Показатель атаки карты (-1 если Х)
        /// </summary>
        public short? Attack { get; set; }
        /// <summary>
        /// Номер карты в своём выпуске
        /// </summary>
        public short? NumberInSet { get; set; }

        public virtual CardsClass? ClassNavigation { get; set; }
        public virtual CardsElement? ElementNavigation { get; set; }
        public virtual CardsRareness? RarenessNavigation { get; set; }
        public virtual CardsSet? SetNameNavigation { get; set; }
        public virtual CardsType? TypeNavigation { get; set; }
        public virtual CardsImage CardsImage { get; set; } = null!;
    }
}
