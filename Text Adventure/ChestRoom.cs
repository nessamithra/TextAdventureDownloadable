using System;

namespace Text_Adventure;

public class ChestRoom : Room
{
    

    private string[] openChestText;
    private string[] findGoldText;
    private string[] loseLifeText;
    private string[] gainLifeText;
    private string[] doNotText;
    
    public bool Open { get; set; }

    public ChestRoom(ref Character chara)
    {
        this.chara = chara;
        this.choiceProText = new string[]
        {
            "Open chest",
            "Give in to your greed",
            "Risk a look"
        };
        this.choiceContraText = new string[]
        {
            "Leave chest",
            "Run away from chest",
            "Do not risk a thing and go to the next room"
        };
        this.appearText = new[]
        {
            "You found a chest!",
            "In the next room you find a out of place looking chest.",
            "You stumble over a chest. Will you open it and see what's inside?"
        };
        openChestText = new[]
        {
            "You open the chest.",
            "Gave in to your greed.",
            "You slowly come closer and lift the lid...",
            "Curious you look inside the chest."
        };
        findGoldText = new[]
        {
            $"You found {chara.Level + 2} gold.",
            $"In the Chest you found {chara.Level + 2} gold.",
            $"You got {chara.Level + 2} gold.",
            $"Pretty big chest for just {chara.Level + 2} gold..."
        };
        loseLifeText = new[]
        {
            $"You lost {chara.Level} HP.",
            $"The chest explodes and you lost {chara.Level} HP.",
            $"Oh no, the chest is poisoned. You lost {chara.Level} HP.",
            $"Oh no, the chest was trapped and you lost {chara.Level} HP."
        };
        gainLifeText = new[]
        {
            $"You gain {chara.Level} HP.",
            $"You found a supe which healed you {chara.Level} HP.",
            $"Luckily you found a potion that healed you {chara.Level} HP.",
            $"You freed a fairy that was trapped in the chest. Thankfully she healed you {chara.Level} HP."
        };
        doNotText = new[]
        {
            "You feel something bad about this chest and leave it.",
            "This chest can't be good. You walk around it and into the next room.",
            "Is that blood coming out of the corners? You better go on quickly.",
            "A bad premonition comes over you and you move into the next room."
        };
    }

    /*
     * Open chest
     */
    public override string[] DoIt()
    {
        string[] story = new string[2];
        story[0] = openChestText[r.Next(openChestText.Length)];
        switch (r.Next(4))
        {
            case 0:
            case 1:
                chara.Gold += chara.Level + 2;
                story[1] = findGoldText[r.Next(findGoldText.Length)];
                break;
            case 2:
                try
                {
                    chara.HP -= chara.Level;
                }
                catch (PlayerIsDeadException e)
                {
                    throw e;
                }

                story[1] = loseLifeText[r.Next(loseLifeText.Length)];
                break;
            case 3:
                chara.HP += chara.Level;
                // Todo: Add different story lines 
                story[1] = gainLifeText[r.Next(gainLifeText.Length)];
                break;
            default:
                break;
        }

        Open = true;
        return story;
    }

    /*
     * Leave room
     */
    public override string DoNot()
    {
        return doNotText[r.Next(doNotText.Length)];
    }
}