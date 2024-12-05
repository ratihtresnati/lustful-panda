using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestSystem 
{
    public string questName;
    public string questDescription;
    // public int goldReward;
    // public int expReward;
    public Objective objective;
    public short questCategory;
    public bool completed;


    [System.Serializable]
    public class Objective
    {
        public enum Type { kill, talk, collect, trigger }
        public int objectiveId;
        public int amount;
        // [System.NonSerialized]
        public int currentAmount;
        public int stageNum;
        public Type type;

        //ini untuk kill
        public bool CheckObjectiveCompleted(Type type, int id) {
            if (this.type == type && id == objectiveId)
                currentAmount++;
            return currentAmount >= amount;
        }

        //untuk collect
        public bool ForceAddObjective(int amount) {
            currentAmount += amount;
            return currentAmount >= amount;
        }

        public bool CheckTriggerObjectiveCompleted(int amount) {
            currentAmount += amount;
            return currentAmount >= amount;
        }

        public void UpdateStageNum(int stage)
        {
            stageNum += stage;
        }

        public override string ToString() {
            switch (type) {
                case Type.kill:
                    return "Kill " + /* MonsterList.MonsterNameFromID(objectiveId) + " " +*/ currentAmount + "/" + amount;
                case Type.talk:
                    return "Talk to " /*+ NpcList.NpcNameFromID(objectiveId) */;
               case Type.collect:
                    return "Collect " + /* ItemList.ItemNameFromID(objectiveId) + " " +*/ currentAmount + "/" + amount;
            }
            return "";
        }
    }

}
