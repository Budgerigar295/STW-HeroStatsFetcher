using CUE4Parse.FileProvider;
using CUE4Parse.MappingsProvider;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports.Engine;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Objects.Core.i18N;
using CUE4Parse.UE4.Objects.Engine.Curves;
using CUE4Parse.UE4.Objects.GameplayTags;
using CUE4Parse.UE4.Objects.UObject;
using CUE4Parse.UE4.Versions;
using System.Data;
using CUE4Parse.GameTypes.FN.Enums;
namespace ConsoleApp6
{

    internal class Program
    {
        private const string pathToFortniteGame = @"C:\\Program Files\\Epic Games\\Fortnite\\FortniteGame\\Content\\Paks"; //Default Game Install Location
        private const string Mappings = @"PATH_TO_YOUR_MAPPINGS"; //Mappings from FModel Folder
        private const string aesKey = "0xB11869C5F61DD3C04431C80AD97D92CD05963B39875E0EE488C247D54722F73C"; //Fortnite: Chapter 3 - Season 4 (22.40)
        private const string loadHeroStats = "FortniteGame/Content/Balance/DataTables/AttributesHeroScaling"; //Load Save the World Hero CurveTable
        public static float calcStat(string AttributeCategoryKey, string AttributeSubcategoryKey, string Rarity, string Tier, string FortStatType, int Time) //Strings Dynamically change based on user input
        {
            float stat = 0;
            var Provider = new DefaultFileProvider(pathToFortniteGame, SearchOption.AllDirectories, true, new VersionContainer(EGame.GAME_UE5_1));
            Provider.MappingsContainer = new FileUsmapTypeMappingsProvider(Mappings);
            Provider.Initialize();
            Provider.SubmitKey(new(0), new(aesKey));
            var table = Provider.LoadObject<UCurveTable>(loadHeroStats);
            foreach (var (key, val) in table.RowMap)
            {

                if (key.Text == AttributeCategoryKey + "." + AttributeSubcategoryKey + "_" + Rarity + "_" + Tier + "." + FortStatType)
                {
                    stat = new FSimpleCurve(val).Eval(Time);
                    return stat;

                }
                else
                {

                }

            }
            return stat;

        }
        public static float minStat(string AttributeInitCategory, string AttributeInitSubcategory, string Rarity, string Tier, string FortStatType)
        {
            float result = 0;
            if (Tier == "T1")
            {
                result = calcStat(AttributeInitCategory, AttributeInitSubcategory, Rarity, Tier, FortStatType, 1);
            }
            else if (Tier == "T2")
            {
                result = calcStat(AttributeInitCategory, AttributeInitSubcategory, Rarity, Tier, FortStatType, 16);
            }
            else if (Tier == "T3")
            {
                result = calcStat(AttributeInitCategory, AttributeInitSubcategory, Rarity, Tier, FortStatType, 33);
            }
            else if (Tier == "T4")
            {
                result = calcStat(AttributeInitCategory, AttributeInitSubcategory, Rarity, Tier, FortStatType, 50);
            }
            else if (Tier == "T5")
            {
                result = calcStat(AttributeInitCategory, AttributeInitSubcategory, Rarity, Tier, FortStatType, 66);
            }
            else
            {
                result = 0;
            }

            return result;

        }
        public static float maxStat(string AttributeInitCategory, string AttributeInitSubcategory, string Rarity, string Tier, string FortStatType)
        {
            float result = 0;
            if (Tier == "T1")
            {
                result = calcStat(AttributeInitCategory, AttributeInitSubcategory, Rarity, Tier, FortStatType, 16);
            }
            else if (Tier == "T2")
            {
                result = calcStat(AttributeInitCategory, AttributeInitSubcategory, Rarity, Tier, FortStatType, 33);
            }
            else if (Tier == "T3")
            {
                result = calcStat(AttributeInitCategory, AttributeInitSubcategory, Rarity, Tier, FortStatType, 50);
            }
            else if (Tier == "T4")
            {
                result = calcStat(AttributeInitCategory, AttributeInitSubcategory, Rarity, Tier, FortStatType, 66);
            }
            else if (Tier == "T5")
            {
                result = calcStat(AttributeInitCategory, AttributeInitSubcategory, Rarity, Tier, FortStatType, 86);
            }
            else
            {
                result = 0;
            }

            return result;
            Console.WriteLine(result);

        }
        
        public static void Uncommon(string AttributeInitCategory, string AttributeInitSubcategory)
        {
            //Health
            float HealthT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T1", "FortHealthSet.MaxHealth");
            float HealthT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T1", "FortHealthSet.MaxHealth");
            float HealthT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T2", "FortHealthSet.MaxHealth");
            float HealthT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T2", "FortHealthSet.MaxHealth");
            float HealthT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T3", "FortHealthSet.MaxHealth");
            float HealthT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T3", "FortHealthSet.MaxHealth");
            //Shield
            float ShieldT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T1", "FortHealthSet.Shield");
            float ShieldT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T1", "FortHealthSet.Shield");
            float ShieldT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T2", "FortHealthSet.Shield");
            float ShieldT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T2", "FortHealthSet.Shield");
            float ShieldT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T3", "FortHealthSet.Shield");
            float ShieldT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T3", "FortHealthSet.Shield");
            //Shield Regen Rate
            float ShieldRegenT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T1", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T1", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T2", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T2", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T3", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T3", "FortRegenHealthSet.ShieldRegenRate");
            //Ability Damage Multiplier
            float AbilityT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T1", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T1", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T2", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T2", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T3", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T3", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            //Hero Healing Multiplier
            float HealingT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T1", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T1", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T2", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T2", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T3", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "UC", "T3", "FortHealthSet.HealingSourceBaseMultiplier");
            //Write Output
            Console.WriteLine("Health: " + HealthT1Min + " / " + HealthT1Max + " / " + HealthT2Min + " / " + HealthT2Max + " / " + HealthT3Min + " / " + HealthT3Max);
            Console.WriteLine("Shield: " + ShieldT1Min + " / " + ShieldT1Max + " / " + ShieldT2Min + " / " + ShieldT2Max + " / " + ShieldT3Min + " / " + ShieldT3Max);
            Console.WriteLine("Shield Regen Rate: " + ShieldRegenT1Min + " / " + ShieldRegenT1Max + " / " + ShieldRegenT2Min + " / " + ShieldRegenT2Max + " / " + ShieldRegenT3Min + " / " + ShieldRegenT3Max);
            Console.WriteLine("Ability Damage Multiplier: " + AbilityT1Min + " / " + AbilityT1Max + " / " + AbilityT2Min + " / " + AbilityT2Max + " / " + AbilityT3Min + " / " + AbilityT3Max);
            Console.WriteLine("Healing Healing Modifier: " + HealingT1Min + " / " + HealingT1Max + " / " + HealingT2Min + " / " + HealingT2Max + " / " + HealingT3Min + " / " + HealingT3Max);
        }
        public static void Rare(string AttributeInitCategory, string AttributeInitSubcategory)
        {
            //Health
            float HealthT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T1", "FortHealthSet.MaxHealth");
            float HealthT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T1", "FortHealthSet.MaxHealth");
            float HealthT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T2", "FortHealthSet.MaxHealth");
            float HealthT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T2", "FortHealthSet.MaxHealth");
            float HealthT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T3", "FortHealthSet.MaxHealth");
            float HealthT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T3", "FortHealthSet.MaxHealth");
            float HealthT4Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T4", "FortHealthSet.MaxHealth");
            float HealthT4Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T4", "FortHealthSet.MaxHealth");
            //Shield
            float ShieldT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T1", "FortHealthSet.Shield");
            float ShieldT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T1", "FortHealthSet.Shield");
            float ShieldT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T2", "FortHealthSet.Shield");
            float ShieldT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T2", "FortHealthSet.Shield");
            float ShieldT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T3", "FortHealthSet.Shield");
            float ShieldT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T3", "FortHealthSet.Shield");
            float ShieldT4Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T4", "FortHealthSet.Shield");
            float ShieldT4Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T4", "FortHealthSet.Shield");
            //Shield Regen Rate
            float ShieldRegenT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T1", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T1", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T2", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T2", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T3", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T3", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT4Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T4", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT4Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T4", "FortRegenHealthSet.ShieldRegenRate");
            //Ability Damage Multiplier
            float AbilityT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T1", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T1", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T2", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T2", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T3", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T3", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT4Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T4", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT4Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T4", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            //Hero Healing Multiplier
            float HealingT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T1", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T1", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T2", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T2", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T3", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T3", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT4Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T4", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT4Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "R", "T4", "FortHealthSet.HealingSourceBaseMultiplier");
            //Write Output
            Console.WriteLine("Health: " + HealthT1Min + " / " + HealthT1Max + " / " + HealthT2Min + " / " + HealthT2Max + " / " + HealthT3Min + " / " + HealthT3Max + " / " + HealthT4Min + " / " + HealthT4Max);
            Console.WriteLine("Shield: " + ShieldT1Min + " / " + ShieldT1Max + " / " + ShieldT2Min + " / " + ShieldT2Max + " / " + ShieldT3Min + " / " + ShieldT3Max + " / " + ShieldT4Min + " / " + ShieldT4Max);
            Console.WriteLine("Shield Regen Rate: " + ShieldRegenT1Min + " / " + ShieldRegenT1Max + " / " + ShieldRegenT2Min + " / " + ShieldRegenT2Max + " / " + ShieldRegenT3Min + " / " + ShieldRegenT3Max + " / " + ShieldRegenT4Min + " / " + ShieldRegenT4Max);
            Console.WriteLine("Ability Damage Multiplier: " + AbilityT1Min + " / " + AbilityT1Max + " / " + AbilityT2Min + " / " + AbilityT2Max + " / " + AbilityT3Min + " / " + AbilityT3Max + " / " + AbilityT4Min + " / " + AbilityT4Max);
            Console.WriteLine("Healing Healing Modifier: " + HealingT1Min + " / " + HealingT1Max + " / " + HealingT2Min + " / " + HealingT2Max + " / " + HealingT3Min + " / " + HealingT3Max + " / " + HealingT4Min + " / " + HealingT4Max);
        }
        public static void Epic(string AttributeInitCategory, string AttributeInitSubcategory)
        {
            //Health
            float HealthT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T1", "FortHealthSet.MaxHealth");
            float HealthT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T1", "FortHealthSet.MaxHealth");
            float HealthT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T2", "FortHealthSet.MaxHealth");
            float HealthT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T2", "FortHealthSet.MaxHealth");
            float HealthT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T3", "FortHealthSet.MaxHealth");
            float HealthT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T3", "FortHealthSet.MaxHealth");
            float HealthT4Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T4", "FortHealthSet.MaxHealth");
            float HealthT4Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T4", "FortHealthSet.MaxHealth");
            float HealthT5Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T5", "FortHealthSet.MaxHealth");
            float HealthT5Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T5", "FortHealthSet.MaxHealth");
            //Shield
            float ShieldT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T1", "FortHealthSet.Shield");
            float ShieldT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T1", "FortHealthSet.Shield");
            float ShieldT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T2", "FortHealthSet.Shield");
            float ShieldT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T2", "FortHealthSet.Shield");
            float ShieldT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T3", "FortHealthSet.Shield");
            float ShieldT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T3", "FortHealthSet.Shield");
            float ShieldT4Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T4", "FortHealthSet.Shield");
            float ShieldT4Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T4", "FortHealthSet.Shield");
            float ShieldT5Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T5", "FortHealthSet.Shield");
            float ShieldT5Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T5", "FortHealthSet.Shield");
            //Shield Regen Rate
            float ShieldRegenT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T1", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T1", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T2", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T2", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T3", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T3", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT4Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T4", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT4Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T4", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT5Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T5", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT5Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T5", "FortRegenHealthSet.ShieldRegenRate");
            //Ability Damage Multiplier
            float AbilityT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T1", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T1", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T2", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T2", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T3", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T3", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT4Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T4", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT4Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T4", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT5Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T5", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT5Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T5", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            //Hero Healing Multiplier
            float HealingT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T1", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T1", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T2", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T2", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T3", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T3", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT4Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T4", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT4Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T4", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT5Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T5", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT5Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "VR", "T5", "FortHealthSet.HealingSourceBaseMultiplier");
            //Write Output
            Console.WriteLine("Health: " + HealthT1Min + " / " + HealthT1Max + " / " + HealthT2Min + " / " + HealthT2Max + " / " + HealthT3Min + " / " + HealthT3Max + " / " + HealthT4Min + " / " + HealthT4Max + " / " + HealthT5Min + " / " + HealthT5Max);
            Console.WriteLine("Shield: " + ShieldT1Min + " / " + ShieldT1Max + " / " + ShieldT2Min + " / " + ShieldT2Max + " / " + ShieldT3Min + " / " + ShieldT3Max + " / " + ShieldT4Min + " / " + ShieldT4Max + " / " + ShieldT5Min + " / " + ShieldT5Max);
            Console.WriteLine("Shield Regen Rate: " + ShieldRegenT1Min + " / " + ShieldRegenT1Max + " / " + ShieldRegenT2Min + " / " + ShieldRegenT2Max + " / " + ShieldRegenT3Min + " / " + ShieldRegenT3Max + " / " + ShieldRegenT4Min + " / " + ShieldRegenT4Max + ShieldRegenT5Min + " / " + ShieldRegenT5Max);
            Console.WriteLine("Ability Damage Multiplier: " + AbilityT1Min + " / " + AbilityT1Max + " / " + AbilityT2Min + " / " + AbilityT2Max + " / " + AbilityT3Min + " / " + AbilityT3Max + " / " + AbilityT4Min + " / " + AbilityT4Max + " / " + AbilityT5Min + " / " + AbilityT5Max);
            Console.WriteLine("Healing Healing Modifier: " + HealingT1Min + " / " + HealingT1Max + " / " + HealingT2Min + " / " + HealingT2Max + " / " + HealingT3Min + " / " + HealingT3Max + " / " + HealingT4Min + " / " + HealingT4Max + " / " + HealingT5Min + " / " + HealingT5Max);

        }
        public static void LegendaryandMythic(string AttributeInitCategory, string AttributeInitSubcategory)
        {
            //Health
            float HealthT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T1", "FortHealthSet.MaxHealth");
            float HealthT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T1", "FortHealthSet.MaxHealth");
            float HealthT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T2", "FortHealthSet.MaxHealth");
            float HealthT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T2", "FortHealthSet.MaxHealth");
            float HealthT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T3", "FortHealthSet.MaxHealth");
            float HealthT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T3", "FortHealthSet.MaxHealth");
            float HealthT4Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T4", "FortHealthSet.MaxHealth");
            float HealthT4Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T4", "FortHealthSet.MaxHealth");
            float HealthT5Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T5", "FortHealthSet.MaxHealth");
            float HealthT5Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T5", "FortHealthSet.MaxHealth");
            //Shield
            float ShieldT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T1", "FortHealthSet.Shield");
            float ShieldT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T1", "FortHealthSet.Shield");
            float ShieldT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T2", "FortHealthSet.Shield");
            float ShieldT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T2", "FortHealthSet.Shield");
            float ShieldT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T3", "FortHealthSet.Shield");
            float ShieldT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T3", "FortHealthSet.Shield");
            float ShieldT4Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T4", "FortHealthSet.Shield");
            float ShieldT4Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T4", "FortHealthSet.Shield");
            float ShieldT5Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T5", "FortHealthSet.Shield");
            float ShieldT5Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T5", "FortHealthSet.Shield");
            //Shield Regen Rate
            float ShieldRegenT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T1", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T1", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T2", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T2", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T3", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T3", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT4Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T4", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT4Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T4", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT5Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T5", "FortRegenHealthSet.ShieldRegenRate");
            float ShieldRegenT5Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T5", "FortRegenHealthSet.ShieldRegenRate");
            //Ability Damage Multiplier
            float AbilityT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T1", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T1", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T2", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T2", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T3", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T3", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT4Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T4", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT4Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T4", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT5Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T5", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            float AbilityT5Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T5", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier");
            //Hero Healing Multiplier
            float HealingT1Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T1", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT1Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T1", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT2Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T2", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT2Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T2", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT3Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T3", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT3Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T3", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT4Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T4", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT4Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T4", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT5Min = minStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T5", "FortHealthSet.HealingSourceBaseMultiplier");
            float HealingT5Max = maxStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T5", "FortHealthSet.HealingSourceBaseMultiplier");
            //Write Output
            Console.WriteLine("Health: " + HealthT1Min + " / " + HealthT1Max + " / " + HealthT2Min + " / " + HealthT2Max + " / " + HealthT3Min + " / " + HealthT3Max + " / " + HealthT4Min + " / " + HealthT4Max + " / " + HealthT5Min + " / " + HealthT5Max);
            Console.WriteLine("Shield: " + ShieldT1Min + " / " + ShieldT1Max + " / " + ShieldT2Min + " / " + ShieldT2Max + " / " + ShieldT3Min + " / " + ShieldT3Max + " / " + ShieldT4Min + " / " + ShieldT4Max + " / " + ShieldT5Min + " / " + ShieldT5Max);
            Console.WriteLine("Shield Regen Rate: " + ShieldRegenT1Min + " / " + ShieldRegenT1Max + " / " + ShieldRegenT2Min + " / " + ShieldRegenT2Max + " / " + ShieldRegenT3Min + " / " + ShieldRegenT3Max + " / " + ShieldRegenT4Min + " / " + ShieldRegenT4Max + " / "+  ShieldRegenT5Min + " / " + ShieldRegenT5Max);
            Console.WriteLine("Ability Damage Multiplier: " + AbilityT1Min + " / " + AbilityT1Max + " / " + AbilityT2Min + " / " + AbilityT2Max + " / " + AbilityT3Min + " / " + AbilityT3Max + " / " + AbilityT4Min + " / " + AbilityT4Max + " / " +  AbilityT5Min + " / " + AbilityT5Max);
            Console.WriteLine("Healing Healing Modifier: " + HealingT1Min + " / " + HealingT1Max + " / " + HealingT2Min + " / " + HealingT2Max + " / " + HealingT3Min + " / " + HealingT3Max + " / " + HealingT4Min + " / " + HealingT4Max + " / " + HealingT5Min + " / " + HealingT5Max);

        }
        public static void Supercharged(string AttributeInitCategory, string AttributeInitSubcategory)
        {
           float Health = calcStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T5", "FortHealthSet.MaxHealth", 100);
           float Shield = calcStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T5", "FortHealthSet.Shield", 100);
           float ShieldRegen = calcStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T5", "FortRegenHealthSet.ShieldRegenRate", 100);
           float Ability = calcStat(AttributeInitCategory, AttributeInitSubcategory, "SR", "T5", "FortDamageSet.OutgoingBaseAbilityDamageMultiplier", 100);
           float Healing = calcStat(AttributeInitCategory, AttributeInitSubcategory, "SR" , "T5" , "FortHealthSet.HealingSourceBaseMultiplier", 100);
           Console.WriteLine("Health: " + Health);
           Console.WriteLine("Shield: " + Shield);
           Console.WriteLine("Shield Regen Rate: " + ShieldRegen);
           Console.WriteLine("Ability Damage Multiplier: " + Ability);
           Console.WriteLine("Hero Healing Modifier: " + Healing);

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Note: It may take a few minutes to load all stats.");
            Console.WriteLine("Enter AttributeInitCategory");
            string AttributeInitCategory = Console.ReadLine();
            Console.WriteLine("Enter AttributeInitSubcategory");
            string AttributeInitSubcategory = Console.ReadLine();
            Console.WriteLine("UNCOMMON");
            Uncommon(AttributeInitCategory, AttributeInitSubcategory);
            Console.WriteLine("RARE");
            Rare(AttributeInitCategory, AttributeInitSubcategory);
            Console.WriteLine("EPIC");
            Epic(AttributeInitCategory, AttributeInitSubcategory);
            Console.WriteLine("LEGENDARY/MYTHIC");
            LegendaryandMythic(AttributeInitCategory, AttributeInitSubcategory);
            Console.WriteLine("SUPERCHARGED");
            Supercharged(AttributeInitCategory, AttributeInitSubcategory);
        }

    }
}
    