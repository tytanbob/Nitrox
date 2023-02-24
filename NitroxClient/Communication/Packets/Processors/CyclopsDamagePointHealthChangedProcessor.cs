﻿using NitroxClient.Communication.Abstract;
using NitroxClient.Communication.Packets.Processors.Abstract;
using NitroxClient.MonoBehaviours;
using NitroxClient.Unity.Helper;
using NitroxModel_Subnautica.Packets;
using UnityEngine;

namespace NitroxClient.Communication.Packets.Processors
{
    public class CyclopsDamagePointHealthChangedProcessor : ClientPacketProcessor<CyclopsDamagePointRepaired>
    {
        private readonly IPacketSender packetSender;

        public CyclopsDamagePointHealthChangedProcessor(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        public override void Process(CyclopsDamagePointRepaired packet)
        {
            GameObject gameObject = NitroxEntity.RequireObjectFrom(packet.Id);
            SubRoot cyclops = gameObject.RequireComponent<SubRoot>();

            using (new PacketSuppressor<CyclopsDamage>())
            using (new PacketSuppressor<CyclopsDamagePointRepaired>())
            {
                cyclops.damageManager.damagePoints[packet.DamagePointIndex].liveMixin.AddHealth(packet.RepairAmount);
            }
        }
    }
}
