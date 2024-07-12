using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ProfileData
    {
        public ProfileSlot profileSlot; 
        public string profileName; 
    }

    public class ProfileModel
    {
        public List<ProfileSlot> profileSlots; 

        public ProfileModel()
        {
            profileSlots = new List<ProfileSlot>();
            for (int i = 0; i < 6; i++)
            {
                profileSlots.Add(new ProfileSlot());
            }
        }

    }
}