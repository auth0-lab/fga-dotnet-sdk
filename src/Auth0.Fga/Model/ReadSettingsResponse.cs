//
// Auth0 Fine Grained Authorization (FGA)/.NET SDK for Auth0 Fine Grained Authorization (FGA)
//
// Auth0 Fine Grained Authorization (FGA) is an early-stage product we are building at Auth0 as part of Auth0Lab to solve fine-grained authorization at scale. If you are interested in learning more about our plans, please reach out via our Discord chat.  The limits and information described in this document is subject to change.
//
// API version: 0.1
// Website: https://fga.dev
// Documentation: https://docs.fga.dev
// Support: https://discord.gg/8naAwJfWN6
// License: [MIT](https://github.com/auth0-lab/fga-dotnet-sdk/blob/main/LICENSE)
//
// NOTE: This file was auto generated. DO NOT EDIT.
//


using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Auth0.Fga.Model {
    /// <summary>
    /// ReadSettingsResponse
    /// </summary>
    [DataContract(Name = "ReadSettingsResponse")]
    public partial class ReadSettingsResponse : IEquatable<ReadSettingsResponse>, IValidatableObject {

        /// <summary>
        /// Gets or Sets Environment
        /// </summary>
        [DataMember(Name = "environment", EmitDefaultValue = false)]
        [JsonPropertyName("environment")]
        public Environment? Environment { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadSettingsResponse" /> class.
        /// </summary>
        /// <param name="environment">environment.</param>
        /// <param name="tokenIssuers">tokenIssuers.</param>
        public ReadSettingsResponse(Environment? environment = default(Environment?), List<TokenIssuer>? tokenIssuers = default(List<TokenIssuer>)) {
            this.Environment = environment;
            this.TokenIssuers = tokenIssuers;
            this.AdditionalProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets or Sets TokenIssuers
        /// </summary>
        [DataMember(Name = "token_issuers", EmitDefaultValue = false)]
        [JsonPropertyName("token_issuers")]
        public List<TokenIssuer> TokenIssuers { get; set; }

        /// <summary>
        /// Gets or Sets additional properties
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties { get; set; }


        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson() {
            return JsonSerializer.Serialize(this);
        }

        /// <summary>
        /// Builds a ReadSettingsResponse from the JSON string presentation of the object
        /// </summary>
        /// <returns>ReadSettingsResponse</returns>
        public static ReadSettingsResponse FromJson(string jsonString) {
            return JsonSerializer.Deserialize<ReadSettingsResponse>(jsonString) ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input) {
            return this.Equals(input as ReadSettingsResponse);
        }

        /// <summary>
        /// Returns true if ReadSettingsResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of ReadSettingsResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ReadSettingsResponse input) {
            if (input == null) {
                return false;
            }
            return
                (
                    this.Environment == input.Environment ||
                    this.Environment.Equals(input.Environment)
                ) &&
                (
                    this.TokenIssuers == input.TokenIssuers ||
                    this.TokenIssuers != null &&
                    input.TokenIssuers != null &&
                    this.TokenIssuers.SequenceEqual(input.TokenIssuers)
                )
                && (this.AdditionalProperties.Count == input.AdditionalProperties.Count && !this.AdditionalProperties.Except(input.AdditionalProperties).Any());
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode() {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 9661;
                hashCode = (hashCode * 9923) + this.Environment.GetHashCode();
                if (this.TokenIssuers != null) {
                    hashCode = (hashCode * 9923) + this.TokenIssuers.GetHashCode();
                }
                if (this.AdditionalProperties != null) {
                    hashCode = (hashCode * 9923) + this.AdditionalProperties.GetHashCode();
                }
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            yield break;
        }
    }

}
