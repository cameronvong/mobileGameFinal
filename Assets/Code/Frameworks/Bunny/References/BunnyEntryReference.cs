using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;
using Bunny.Entries;

namespace Bunny.References
{
    [Serializable]
    public struct BunnyEntryReference : IEquatable<BunnyEntryReference>
    {
        public string id;

        public bool HasValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => string.IsNullOrEmpty(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(BunnyEntryReference other)
        {
            return id == other.id;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return obj is BunnyEntryReference other && Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(BunnyEntryReference lhs, BunnyEntryReference rhs)
        {
            return lhs.id == rhs.id;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(BunnyEntryReference lhs, BunnyEntryReference rhs)
        {
            return lhs.id != rhs.id;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator string(BunnyEntryReference reference)
        {
            return reference.id;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BunnyEntryReference(string identifier)
        {
            return new BunnyEntryReference(identifier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BunnyEntryReference(BunnyBaseEntry entry)
        {
            return new BunnyEntryReference(entry.id);
        }

        public BunnyEntryReference(string identifier)
        {
            if(string.IsNullOrEmpty(identifier))
            {
                throw new ArgumentOutOfRangeException(nameof(identifier), "ID cannot be empty");
            }
            id = identifier;
        }
    }
}