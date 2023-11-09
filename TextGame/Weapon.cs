using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace TextGame
{
    class Weapon : Item
    {
        public Weapon(string name, string dscr, int price, int atk, int def) : base(name, dscr, price, atk, def)
        {

        }
        public override Item DeepCopy()
        {
            return new Weapon(Name, Dscr, Price * 85 / 100, Atk, Def);
        }
        public override void GetStatus(out int status)
        {
            status = Atk;
        }
        public override Pack GetTypeEnum()
        {
            return Pack.Weapon;
        }
        public override void PrintItemInfo(ref ConsoleTable table, int itemNumber, bool isShop)
        {
            string equipMark = "";
            if (itemNumber > 0)
                equipMark += itemNumber.ToString() + ". ";
            if (IsEquip)
                equipMark += "[E] ";
            if (isShop)
            {
                if (Price > -1)
                    table.AddRow(equipMark + Name, "공격력 +" + Atk, Dscr, Price.ToString() + " G");
                else
                    table.AddRow(equipMark + Name, "공격력 +" + Atk, Dscr, "구매 완료");
            }
            else
                table.AddRow(equipMark + Name, "공격력 +" + Atk, Dscr);
        }
    }
}
