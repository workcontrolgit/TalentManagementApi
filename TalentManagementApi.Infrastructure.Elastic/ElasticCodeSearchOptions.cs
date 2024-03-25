namespace TalentManagementApi.Infrastructure.Search
{
    /// <summary>
    /// Elasticsearch options.
    /// </summary>
    public class ElasticCodeSearchOptions
    {
        /// <summary>
        /// Endpoint of the Elasticsearch Node.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Index to use for Code Search.
        /// </summary>
        public string IndexName { get; set; }

        /// <summary>
        /// Elasticsearch Username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Elasticsearch Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Certificate Fingerprint for trusting the Certificate.
        /// </summary>
        public string CertificateFingerprint { get; set; }
    }
}