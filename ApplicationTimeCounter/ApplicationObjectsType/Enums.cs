﻿namespace ApplicationTimeCounter
{
    public enum DialogWindowsState
    {
        YesCancel = 0,
        Ok = 1,
    }

    public enum DialogWindowsMessage
    {
        DeleteAllApplicationsWithAcitivty = 0,
        DeleteOneApplicationWithAcitivty = 1,
        DeleteAcitivty = 2,
        EditNameDefaultActivity = 3,
        DeleteDefaultActivity = 4,
        DeleteGroup = 5
    }

    public enum AddTo
    {
        Activity = 1,
        Group = 2
    }
}