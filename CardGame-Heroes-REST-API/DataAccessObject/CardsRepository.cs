using Microsoft.EntityFrameworkCore;
using CardGame_Heroes_REST_API.Models;

namespace CardGame_Heroes_REST_API.DataAccessObject
{
    
    public class CardsRepository
    {
        private readonly Database.CardsDbContext _context;
        private ILogger<CardsRepository> _logger;
        
        public CardsRepository(Database.CardsDbContext context, ILogger<CardsRepository> logger)
        {
            this._context = context;
            _logger = logger;
        }

        public List<Card> Get(Card cardInfoParams)
        {
            if (!_context.Set<Card>().Any()) return null;
            var queryList = new List<IQueryable<Card>?>();

            cardInfoParams.Class = ChangeSeparator('|', cardInfoParams.Class);
            cardInfoParams.Element = ChangeSeparator('|', cardInfoParams.Element);

            if(cardInfoParams.Class != null && cardInfoParams.Class.Contains("|"))
                foreach(var @class in cardInfoParams.Class.Split("|"))
                {
                    var splitedCard = new Card
                    {
                        Name = cardInfoParams.Name,
                        Class = @class,
                        Element = cardInfoParams.Element,
                        Type = cardInfoParams.Type,
                        Rareness = cardInfoParams.Rareness,
                        SetName = cardInfoParams.SetName,   
                        Cost = cardInfoParams.Cost, 
                        Health = cardInfoParams.Health, 
                        Attack = cardInfoParams.Attack
                    };
                    queryList.Add(FindCard(splitedCard));
                }

            if (cardInfoParams.Element != null && cardInfoParams.Element.Contains("|"))
                foreach (var element in cardInfoParams.Element.Split("|"))
                {
                    var splitedCard = new Card
                    {
                        Name = cardInfoParams.Name,
                        Class = cardInfoParams.Class,
                        Element = element,
                        Type = cardInfoParams.Type,
                        Rareness = cardInfoParams.Rareness,
                        SetName = cardInfoParams.SetName,
                        Cost = cardInfoParams.Cost,
                        Health = cardInfoParams.Health,
                        Attack = cardInfoParams.Attack
                    };
                    queryList.Add(FindCard(splitedCard));
                }

            queryList.Add(FindCard(cardInfoParams));

            IEnumerable<Card> response = new List<Card>().AsEnumerable();

            queryList.ForEach(query =>
            {
                List<Card>? collection = query.ToList();
                var list = response.ToList();
                list.AddRange(collection);
                response = list.AsEnumerable();
            });

            response = response.Distinct();

            if (cardInfoParams.Name != null) 
                response = response.Where(x => x.Name.ToLower().Contains(cardInfoParams.Name.ToLower()));
            if (cardInfoParams.Attack != null)
                response = response.Where(x => x.Attack == cardInfoParams.Attack);
            if (cardInfoParams.Cost != null)
                response = response.Where(x => x.Cost == cardInfoParams.Cost);
            if (cardInfoParams.Health != null)
                response = response.Where(x => x.Health == cardInfoParams.Health);
            if (cardInfoParams.Element != null)
                if (cardInfoParams.Element.Contains("|"))
                    response = response.Where(x => 
                    x.Element.ToLower().Split("|").All(s => cardInfoParams.Element.ToLower().Split("|").Contains(s))
                    && x.Element.ToLower().Split("|").Length >= cardInfoParams.Element.ToLower().Split("|").Length);
                else response = response.Where(x => x.Element.ToLower().Split("|").Any(x => x == cardInfoParams.Element.ToLower()));
            if (cardInfoParams.Class != null)
                if (cardInfoParams.Class.Contains("|"))
                    response = response.Where(x => 
                    x.Class.ToLower().Split("|").All(s => cardInfoParams.Class.ToLower().Split("|").Contains(s)) 
                    && x.Class.ToLower().Split("|").Length >= cardInfoParams.Class.ToLower().Split("|").Length);
                else response = response.Where(x => x.Class.ToLower().Split("|").Any(x => x == cardInfoParams.Class.ToLower()));
            if (cardInfoParams.Rareness != null)
                response = response.Where(x => x.Rareness.ToLower() == cardInfoParams.Rareness.ToLower());
            if (cardInfoParams.Type != null)
                response = response.Where(x => x.Type.ToLower() == cardInfoParams.Type.ToLower());
            if (cardInfoParams.SetName != null)
                response = response.Where(x => x.SetName.ToLower() == cardInfoParams.SetName.ToLower());

            var list = response.ToList();

            return list;
        }

        private IQueryable<Card>? FindCard(Card card)
        {
            var query = _context.Cards.Where(model => card.Name != null && model.Name.ToLower().Contains(card.Name.ToLower())
                                       || card.Attack != null && model.Attack == card.Attack
                                       || card.Cost != null && model.Cost == card.Cost
                                       || card.Health != null && model.Health == card.Health
                                       || card.Element != null && model.Element.ToLower().Contains(card.Element.ToLower())
                                       || card.Class != null && model.Class.ToLower().Contains(card.Class.ToLower())
                                       || card.Rareness != null && model.Rareness.ToLower() == card.Rareness.ToLower()
                                       || card.Type != null && model.Type.ToLower() == card.Type.ToLower()
                                       || card.SetName != null && model.SetName.ToLower() == card.SetName.ToLower());

            query.Include(nav => nav.SetNameNavigation).Include(nav => nav.CardsImage).ToList();

            return query;
        }
        
        public List<string> GetClasses()
        {
            var names = _context.CardsClasses.Select(x => x.ClassName).AsEnumerable().ToList();
            var elementToRemove = new List<string>();   

            foreach (var name in names)
                if (name.Split("|").Length > 1) elementToRemove.Add(name);

            elementToRemove.ForEach(i => names.Remove(i));

            return names;
        }
        public List<string> GetTypes() => _context.CardsTypes.Select(x => x.TypeName).ToList();
        public List<string> GetElements()
        {
            var names = _context.CardsElements.Select(x => x.ElementName).AsEnumerable().ToList();
            var elementToRemove = new List<string>();

            foreach (var name in names)
                if (name.Split("|").Length > 1) elementToRemove.Add(name);

            elementToRemove.ForEach(i => names.Remove(i));

            return names;
        }
        public List<CardsImage> GetImagesURLs(uint id, string name) =>
            _context.CardsImages.Where(model => id != null && model.CardId == id 
                                            || name != null && model.CardName == name).AsEnumerable().ToList();
        public List<string> GetRarenesses() => _context.CardsRarenesses.Select(x => x.RarenessName).ToList();
        public List<CardsSet> GetSets()
        {
            var sets = _context.CardsSets.ToList();
            foreach (var element in sets)        
                element.Cards = null;

            return sets;
        }

        private static string ChangeSeparator(char newSeparator, string @string)
        {
            if (@string == null) 
                return @string;

            @string = @string.Replace('|', newSeparator);
            @string = @string.Replace(',',newSeparator);
            @string = @string.Replace(';',newSeparator);
            @string = @string.Replace(':',newSeparator);
            @string = @string.Replace('\\',newSeparator);
            @string = @string.Replace('/',newSeparator);
            @string = @string.Replace('_',newSeparator);
            @string = @string.Replace('+',newSeparator);
            @string = @string.Replace('.', newSeparator);
            @string = @string.Replace('*', newSeparator);

            return @string;
        }
    }
}
    
