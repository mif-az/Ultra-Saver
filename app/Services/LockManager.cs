using System.Collections.Concurrent;

namespace Ultra_Saver;

public static class LockManager
{
    private class lockObject
    {
        public uint count { get; set; } = 1;
    }

    private static ConcurrentDictionary<string, lockObject> _locks = new ConcurrentDictionary<string, lockObject>();

    private static lockObject GetLock(string lockName)
    {

        lockObject res;

        if (_locks.TryGetValue(lockName, out res))
        {
            res.count++;
            return res;
        }

        res = new lockObject();
        _locks.TryAdd(lockName, res);
        return res;
    }

    public static void GetLock(string lockName, Action action)
    {
        lock (GetLock(lockName))
        {
            action();
            UnLock(lockName);
        }
    }

    private static void UnLock(string lockName)
    {
        lockObject res;
        if (_locks.TryGetValue(lockName, out res))
        {
            res.count--;
            if (res.count == 0)
            {
                _locks.TryRemove(KeyValuePair.Create(lockName, res));
            }
        }
    }

}