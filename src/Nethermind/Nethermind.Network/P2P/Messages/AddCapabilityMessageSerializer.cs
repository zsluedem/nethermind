//  Copyright (c) 2021 Demerzel Solutions Limited
//  This file is part of the Nethermind library.
// 
//  The Nethermind library is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  The Nethermind library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with the Nethermind. If not, see <http://www.gnu.org/licenses/>.

using Nethermind.Serialization.Rlp;
using Nethermind.Stats.Model;

namespace Nethermind.Network.P2P.Messages
{
    /// <summary>
    /// This is probably used in NDM
    /// </summary>
    public class AddCapabilityMessageSerializer : IMessageSerializer<AddCapabilityMessage>
    {
        public byte[] Serialize(AddCapabilityMessage msg)
            => Rlp.Encode(Rlp.Encode(msg.Capability.ProtocolCode.ToLowerInvariant()),
                Rlp.Encode(msg.Capability.Version)).Bytes;

        public AddCapabilityMessage Deserialize(byte[] msgBytes)
        {
            RlpStream context = msgBytes.AsRlpStream();
            context.ReadSequenceLength();
            string protocolCode = context.DecodeString();
            byte version = context.DecodeByte();

            return new AddCapabilityMessage(new Capability(protocolCode, version));
        }
    }
}
