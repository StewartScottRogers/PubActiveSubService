using System;

namespace PubActiveSubService.Models {

    [Serializable]
    public class Results {
        public string Result = string.Empty;
        public bool Success = false;

        public override string ToString() => $"{nameof(Success)}: {Success}, {nameof(Result)}: {Result}.";
    }
}
