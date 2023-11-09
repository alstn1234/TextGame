using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace TextGame
{
    class Armor : Item
    {
        public Armor(string name, string dscr, int price, int atk, int def) : base(name, dscr, price, atk, def)
        {

        }
        public override Item DeepCopy()
        {
            return new Armor(Name, Dscr, Price * 85 / 100, Atk, Def);
        }
        public override void GetStatus(out int status)
        {
            status = Def;
        }
        public override Pack GetTypeEnum()
        {
            return Pack.Armor;
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
                    table.AddRow(equipMark + Name, "방어력 +" + Def, Dscr, Price.ToString() + " G");
                else
                    table.AddRow(equipMark + Name, "방어력 +" + Def, Dscr, "구매 완료");
            }
            else
                table.AddRow(equipMark + Name, "방어력 +" + Def, Dscr);
        }
    }
}
