using Netcode;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfCheckout
{
    internal static class Extensions
    {
        public static NPC Find(this NetCollection<NPC> npcs, string name)
        {
            NPC npc;
            using (NetCollection<NPC>.Enumerator enumarator = npcs.GetEnumerator())
            {
                while (true)
                {
                    if (enumarator.MoveNext())
                    {
                        NPC npc2 = enumarator.Current;
                        if (npc2.Name != name)
                            continue;

                        npc = npc2;
                    }
                    else
                        return null;

                    break;
                }
            }

            return npc;
        }

        public static GameLocation Find(this IList<GameLocation> locations, string name)
        {
            GameLocation location;
            using (IEnumerator<GameLocation> enumerator = locations.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        GameLocation current = enumerator.Current;
                        if (current.Name != name)
                            continue;

                        location = current;
                    }
                    else
                        return null;

                    break;
                }
            }

            return location;
        }
    }
}
