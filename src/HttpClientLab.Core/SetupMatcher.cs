using System;

namespace HttpClientLab
{
    public class SetupMatcher<T, TSetup> : Setup<TSetup>
    {
        protected readonly Func<T, bool> _matcher;

        internal bool Match(T clientName)
        {
            return _matcher(clientName);
        }
        protected SetupMatcher(Func<T, bool> matcher) : base()
        {
            _matcher = matcher;
        }
    }

    public class Matcher<T>
    {
        protected readonly Func<T, bool> _matcher;

        internal bool Match(T clientName)
        {
            return _matcher(clientName);
        }
        protected Matcher(Func<T, bool> matcher) : base()
        {
            _matcher = matcher;
        }
    }
}
