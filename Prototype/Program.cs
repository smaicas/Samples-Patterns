using Newtonsoft.Json;

CharacterTank ch1 = new()
{
    Name = "M.A Barracus",
    CreatedDate = DateTime.Now,
    Attributes = new CharacterAttributes()
    {
        Level = 30
    }
};

Character ch2 = ch1.Clone();

Character ch3 = ch1.DeepClone();

Console.WriteLine(ch1.ToString());
Console.Write(Environment.NewLine);
Console.WriteLine(ch2.ToString());
Console.Write(Environment.NewLine);
Console.WriteLine(ch3.ToString());
Console.Write(Environment.NewLine);

ch1.Attributes.Level = 41;

CharacterArcher ch4 = new()
{
    Name = "I am not Légolas",
    CreatedDate = DateTime.Now,
};

Console.WriteLine(ch1.ToString());
Console.Write(Environment.NewLine);
Console.WriteLine(ch2.ToString());
Console.Write(Environment.NewLine);
Console.WriteLine(ch3.ToString());
Console.Write(Environment.NewLine);
Console.WriteLine(ch4.ToString());

abstract class Character
{
    public string Name { get; set; }
    public abstract CharacterType Type { get; }
    public DateTime CreatedDate { get; set; }
    public CharacterAttributes Attributes { get; set; }

    public virtual Character Clone() => (Character)this.MemberwiseClone();

    public abstract Character DeepClone();

    public new virtual string ToString() => JsonConvert.SerializeObject(this);
}

internal class CharacterAttributes
{
    public int Level { get; set; }
}

class CharacterTank : Character
{
    public string SpecificForTank { get; set; } = string.Empty;
    public override CharacterType Type => new()
    {
        Name = "Tank",
        Damage = 400,
        Defense = 999
    };

    public override Character DeepClone()
    {
        CharacterTank newCharacter = (CharacterTank)this.MemberwiseClone();
        newCharacter.Attributes = Attributes;
        return newCharacter;
    }
}

class CharacterArcher : Character
{
    public override CharacterType Type => new()
    {
        Name = "Archer",
        Damage = 999,
        Defense = 400
    };
    public override Character DeepClone()
    {
        CharacterTank newCharacter = (CharacterTank)this.MemberwiseClone();
        newCharacter.Attributes = Attributes;
        return newCharacter;
    }
}

class CharacterType
{
    public string Name { get; set; }
    public int Damage { get; set; }
    public int Defense { get; set; }
}