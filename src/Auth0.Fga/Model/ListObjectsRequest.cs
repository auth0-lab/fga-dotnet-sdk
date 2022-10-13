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
    /// ListObjectsRequest
    /// </summary>
    [DataContract(Name = "ListObjects_request")]
    public partial class ListObjectsRequest : IEquatable<ListObjectsRequest>, IValidatableObject {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListObjectsRequest" /> class.
        /// </summary>
        [JsonConstructor]
        public ListObjectsRequest() {
            this.AdditionalProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListObjectsRequest" /> class.
        /// </summary>
        /// <param name="authorizationModelId">authorizationModelId.</param>
        /// <param name="type">type.</param>
        /// <param name="relation">relation.</param>
        /// <param name="user">user.</param>
        /// <param name="contextualTuples">contextualTuples.</param>
        public ListObjectsRequest(string authorizationModelId = default(string), string type = default(string), string relation = default(string), string user = default(string), ContextualTupleKeys contextualTuples = default(ContextualTupleKeys)) {
            this.AuthorizationModelId = authorizationModelId;
            this.Type = type;
            this.Relation = relation;
            this.User = user;
            this.ContextualTuples = contextualTuples;
            this.AdditionalProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets or Sets AuthorizationModelId
        /// </summary>
        [DataMember(Name = "authorization_model_id", EmitDefaultValue = false)]
        [JsonPropertyName("authorization_model_id")]
        public string? AuthorizationModelId { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Gets or Sets Relation
        /// </summary>
        [DataMember(Name = "relation", EmitDefaultValue = false)]
        [JsonPropertyName("relation")]
        public string? Relation { get; set; }

        /// <summary>
        /// Gets or Sets User
        /// </summary>
        [DataMember(Name = "user", EmitDefaultValue = false)]
        [JsonPropertyName("user")]
        public string? User { get; set; }

        /// <summary>
        /// Gets or Sets ContextualTuples
        /// </summary>
        [DataMember(Name = "contextual_tuples", EmitDefaultValue = false)]
        [JsonPropertyName("contextual_tuples")]
        public ContextualTupleKeys? ContextualTuples { get; set; }

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
        /// Builds a ListObjectsRequest from the JSON string presentation of the object
        /// </summary>
        /// <returns>ListObjectsRequest</returns>
        public static ListObjectsRequest FromJson(string jsonString) {
            return JsonSerializer.Deserialize<ListObjectsRequest>(jsonString) ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input) {
            return this.Equals(input as ListObjectsRequest);
        }

        /// <summary>
        /// Returns true if ListObjectsRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of ListObjectsRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ListObjectsRequest input) {
            if (input == null) {
                return false;
            }
            return
                (
                    this.AuthorizationModelId == input.AuthorizationModelId ||
                    (this.AuthorizationModelId != null &&
                    this.AuthorizationModelId.Equals(input.AuthorizationModelId))
                ) &&
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
                ) &&
                (
                    this.Relation == input.Relation ||
                    (this.Relation != null &&
                    this.Relation.Equals(input.Relation))
                ) &&
                (
                    this.User == input.User ||
                    (this.User != null &&
                    this.User.Equals(input.User))
                ) &&
                (
                    this.ContextualTuples == input.ContextualTuples ||
                    (this.ContextualTuples != null &&
                    this.ContextualTuples.Equals(input.ContextualTuples))
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
                if (this.Type != null) {
                    hashCode = (hashCode * 9923) + this.Type.GetHashCode();
                }
                if (this.Relation != null) {
                    hashCode = (hashCode * 9923) + this.Relation.GetHashCode();
                }
                if (this.User != null) {
                    hashCode = (hashCode * 9923) + this.User.GetHashCode();
                }
                if (this.ContextualTuples != null) {
                    hashCode = (hashCode * 9923) + this.ContextualTuples.GetHashCode();
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