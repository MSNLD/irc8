﻿namespace Irc.Interfaces;

public interface IModeCollection
{
    IModeRule this[char mode] { get; }
    void SetModeChar(char mode, int value);
    void ToggleModeChar(char mode, bool flag);
    int GetModeChar(char mode);
    string GetModeString();
    IModeRule GetMode(char mode);
    bool HasMode(char mode);
    string GetSupportedModes();
    string ToString();
}