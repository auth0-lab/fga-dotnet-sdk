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

namespace Com.Auth0.FGA.Model {
    /// <summary>
    /// TokenIssuer
    /// </summary>
    [DataContract(Name = "TokenIssuer")]
    public partial class TokenIssuer : IEquatable<TokenIssuer>, IValidatableObject {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenIssuer" /> class.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="issuerUrl">issuerUrl.</param>
        public TokenIssuer(string? id = default(string), string? issuerUrl = default(string)) {
            this.Id = id;
            this.IssuerUrl = issuerUrl;
            this.AdditionalProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets IssuerUrl
        /// </summary>
        [DataMember(Name = "issuer_url", EmitDefaultValue = false)]
        [JsonPropertyName("issuer_url")]
        public string IssuerUrl { get; set; }

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
        /// Builds a TokenIssuer from the JSON string presentation of the object
        /// </summary>
        /// <returns>TokenIssuer</returns>
        public static TokenIssuer FromJson(string jsonString) {
            return JsonSerializer.Deserialize<TokenIssuer>(jsonString) ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input) {
            return this.Equals(input as TokenIssuer);
        }

        /// <summary>
        /// Returns true if TokenIssuer instances are equal
        /// </summary>
        /// <param name="input">Instance of TokenIssuer to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TokenIssuer input) {
            if (input == null) {
                return false;
            }
            return
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) &&
                (
                    this.IssuerUrl == input.IssuerUrl ||
                    (this.IssuerUrl != null &&
                    this.IssuerUrl.Equals(input.IssuerUrl))
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
                if (this.Id != null) {
                    hashCode = (hashCode * 9923) + this.Id.GetHashCode();
                }
                if (this.IssuerUrl != null) {
                    hashCode = (hashCode * 9923) + this.IssuerUrl.GetHashCode();
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
