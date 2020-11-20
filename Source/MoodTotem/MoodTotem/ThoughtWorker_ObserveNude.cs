using System.Collections.Generic;
using Verse;
using RimWorld;

namespace MoodTotem
{
    public class ThoughtWorker_ObserveNude:ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            //return base.CurrentStateInternal(p);
            bool flag = !p.RaceProps.Humanlike;
            int state = -1;
            if (flag)
            {
                return ThoughtState.Inactive;
            }
            else
            {
                List<Pawn> ColonistSpawned = p.Map.mapPawns.FreeColonistsSpawned;
                foreach(Pawn pawn in ColonistSpawned)
                {
                    if (pawn == p)
                    {
                        continue;
                    }
                    if (!p.Position.InHorDistOf(pawn.Position, 12f))
                    {
                        continue;
                    }
                    if (!pawn.apparel.PsychologicallyNude)
                    {
                        continue;
                    }
                    if (!pawn.story.traits.HasTrait(TraitDefOf.Beauty))
                    {
                        if(state>2||state==-1)
                        state=2;
                    }
                    if (pawn.story.traits.DegreeOfTrait(TraitDefOf.Beauty) == 2)
                    {
                        state=0;
                    }
                    else if (pawn.story.traits.DegreeOfTrait(TraitDefOf.Beauty) == 1)
                    {
                        if(state>1 || state == -1)
                        state=1;
                    }
                    else if (pawn.story.traits.DegreeOfTrait(TraitDefOf.Beauty) == -1)
                    {
                        if(state>3 || state == -1)
                        state=3;
                    }
                    else if (pawn.story.traits.DegreeOfTrait(TraitDefOf.Beauty) == -2)
                    {
                        if(state==-1)
                        state=4;
                    }
                }
            }
            if (state != -1)
            {
                return ThoughtState.ActiveAtStage(state);
            }
            return ThoughtState.Inactive;
        }
    }
}
