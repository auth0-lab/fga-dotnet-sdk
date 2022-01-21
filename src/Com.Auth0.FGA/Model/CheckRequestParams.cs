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
    /// CheckRequestParams
    /// </summary>
    [DataContract(Name = "CheckRequestParams")]
    public partial class CheckRequestParams : IEquatable<CheckRequestParams>, IValidatableObject {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckRequestParams" /> class.
        /// </summary>
        /// <param name="tupleKey">tupleKey.</param>
        /// <param name="authorizationModelId">authorizationModelId.</param>
        public CheckRequestParams(TupleKey? tupleKey = default(TupleKey), string? authorizationModelId = default(string)) {
            this.TupleKey = tupleKey;
            this.AuthorizationModelId = authorizationModelId;
            this.AdditionalProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets or Sets TupleKey
        /// </summary>
        [DataMember(Name = "tuple_key", EmitDefaultValue = false)]
        [JsonPropertyName("tuple_key")]
        public TupleKey TupleKey { get; set; }

        /// <summary>
        /// Gets or Sets AuthorizationModelId
        /// </summary>
        [DataMember(Name = "authorization_model_id", EmitDefaultValue = false)]
        [JsonPropertyName("authorization_model_id")]
        public string AuthorizationModelId { get; set; }

        /// <summary>
        /// Defaults to false. Making it true has performance implications.
        /// </summary>
        /// <value>Defaults to false. Making it true has performance implications.</value>
        [DataMember(Name = "trace", EmitDefaultValue = true)]
        [JsonPropertyName("trace")]
        public bool Trace { get; private set; }

        /// <summary>
        /// Returns false as Trace should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeTrace() {
            return false;
        }
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
        /// Builds a CheckRequestParams from the JSON string presentation of the object
        /// </summary>
        /// <returns>CheckRequestParams</returns>
        public static CheckRequestParams FromJson(string jsonString) {
            return JsonSerializer.Deserialize<CheckRequestParams>(jsonString) ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input) {
            return this.Equals(input as CheckRequestParams);
        }

        /// <summary>
        /// Returns true if CheckRequestParams instances are equal
        /// </summary>
        /// <param name="input">Instance of CheckRequestParams to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CheckRequestParams input) {
            if (input == null) {
                return false;
            }
            return
                (
                    this.TupleKey == input.TupleKey ||
                    (this.TupleKey != null &&
                    this.TupleKey.Equals(input.TupleKey))
                ) &&
                (
                    this.AuthorizationModelId == input.AuthorizationModelId ||
                    (this.AuthorizationModelId != null &&
                    this.AuthorizationModelId.Equals(input.AuthorizationModelId))
                ) &&
                (
                    this.Trace == input.Trace ||
                    this.Trace.Equals(input.Trace)
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
                if (this.TupleKey != null) {
                    hashCode = (hashCode * 9923) + this.TupleKey.GetHashCode();
                }
                if (this.AuthorizationModelId != null) {
                    hashCode = (hashCode * 9923) + this.AuthorizationModelId.GetHashCode();
                }
                hashCode = (hashCode * 9923) + this.Trace.GetHashCode();
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
