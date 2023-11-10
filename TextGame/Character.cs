using System;
using System.Collections.Generic;
using System.Text;

namespace TextGame
{
    class Character
    {
        public string Name { get; private set; }
        public string Job { get; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set; }
        public int extraAtk { get; set; }
        public int extraDef { get; set; }
        public int Exp { get; set; }
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
            Exp = 0;
            // 현재 장착 무기와 방어구를 저장
            eqWeapon = null;
            eqArmor = null;
        }

        // 총 공격력 계산하여 return
        public int GetTotalAtk()
        {
            return Atk + extraAtk;
        }
        // 총 방어력 계산하여 return
        public int GetTotalDef()
        {
            return Def + extraDef;
        }
        public void ChangeGold(int price)
        {
            Gold += price;
        }
        public void EquipItem(Item item)
        {
            if (item == null) return;
            // 무기 장착시 장착중인 무기 해제 후 장착
            if (item.GetTypeEnum() == Pack.Weapon)
            {
                eqWeapon?.UnEquip();
                eqWeapon = item;
            }
            // 방어구 장착시 장착중인 무기 해제 후 장착
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
            // 장착 해제
            if (item.GetTypeEnum() == Pack.Weapon)
                eqWeapon = null;
            if (item.GetTypeEnum() == Pack.Armor)
                eqArmor = null;

            extraAtk -= item.Atk;
            extraDef -= item.Def;
        }

        public void ChangeHp(int hpAmount)
        {
            Hp = Math.Clamp(Hp + hpAmount, 0, 100);
            
        }
        public void ChangeExp(int expAmount)
        {
            Exp += expAmount * 2 / Level;
            while(Exp >= 100)
            {
                LevelUp();
                Exp -= 100;
            }
        }
        public void ChangeName(string name)
        {
            Name = name;
        }

        public void LevelUp()
        {
            Level++;
            Atk += 1;
            Def += 1;
            Console.WriteLine("Level Up!");
            Console.WriteLine($"Atk {Atk - 1} -> {Atk}");
            Console.WriteLine($"Atk {Def - 1} -> {Def}");
            Console.WriteLine();
        }
    }
}
