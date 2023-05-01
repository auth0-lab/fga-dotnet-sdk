//
// Auth0 Fine Grained Authorization (FGA)/.NET SDK for Auth0 Fine Grained Authorization (FGA)
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
    /// WriteAuthorizationModelResponse
    /// </summary>
    [DataContract(Name = "WriteAuthorizationModelResponse")]
    public partial class WriteAuthorizationModelResponse : IEquatable<WriteAuthorizationModelResponse>, IValidatableObject {
        /// <summary>
        /// Initializes a new instance of the <see cref="WriteAuthorizationModelResponse" /> class.
        /// </summary>
        [JsonConstructor]
        public WriteAuthorizationModelResponse() {
            this.AdditionalProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteAuthorizationModelResponse" /> class.
        /// </summary>
        /// <param name="authorizationModelId">authorizationModelId.</param>
        public WriteAuthorizationModelResponse(string authorizationModelId = default(string)) {
            this.AuthorizationModelId = authorizationModelId;
            this.AdditionalProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets or Sets AuthorizationModelId
        /// </summary>
        [DataMember(Name = "authorization_model_id", EmitDefaultValue = false)]
        [JsonPropertyName("authorization_model_id")]
        public string? AuthorizationModelId { get; set; }

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
        /// Builds a WriteAuthorizationModelResponse from the JSON string presentation of the object
        /// </summary>
        /// <returns>WriteAuthorizationModelResponse</returns>
        public static WriteAuthorizationModelResponse FromJson(string jsonString) {
            return JsonSerializer.Deserialize<WriteAuthorizationModelResponse>(jsonString) ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input) {
            return this.Equals(input as WriteAuthorizationModelResponse);
        }

        /// <summary>
        /// Returns true if WriteAuthorizationModelResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of WriteAuthorizationModelResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(WriteAuthorizationModelResponse input) {
            if (input == null) {
                return false;
            }
            return
                (
                    this.AuthorizationModelId == input.AuthorizationModelId ||
                    (this.AuthorizationModelId != null &&
                    this.AuthorizationModelId.Equals(input.AuthorizationModelId))
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
                if (this.AuthorizationModelId != null) {
                    hashCode = (hashCode * 9923) + this.AuthorizationModelId.GetHashCode();
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