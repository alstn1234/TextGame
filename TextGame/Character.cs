using System;
using System.Collections.Generic;
using System.Text;

namespace TextGame
{
    class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; set; }
        public int Gold { get; set; }
        public int extraAtk { get; set; }
        public int extraDef { get; set; }
        public Item eqWeapon { get; set; }
        public Item eqArmor { get; set; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
            extraAtk = 0;
            extraDef = 0;
            eqWeapon = null;
            eqArmor = null;
        }
        public int GetTotalAtk()
        {
            return Atk + extraAtk;
        }

        public int GetTotalDef()
        {
            return Def + extraDef;
        }
        public void BuyItem(int price)
        {
            Gold -= price;
        }
        public void SellItem(int price)
        {
            Gold += price;
        }
        public void EquipItem(Item item)
        {
            if (item == null) return;

            if (item.GetTypeEnum() == Pack.Weapon)
            {
                eqWeapon?.UnEquip();
                eqWeapon = item;
            }
            if (item.GetTypeEnum() == Pack.Armor)
            {
                eqArmor?.UnEquip();
                eqArmor = item;
            }

            extraAtk += item.Atk;
            extraDef += item.Def;
        }

        public void UnequipItem(Item item)
        {
            if (item == null) return;

            if (item.GetTypeEnum() == Pack.Weapon)
                eqWeapon = null;
            if (item.GetTypeEnum() == Pack.Armor)
                eqArmor = null;

            extraAtk -= item.Atk;
            extraDef -= item.Def;
        }
    }
}
