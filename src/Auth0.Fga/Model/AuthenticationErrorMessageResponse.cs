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
    /// AuthenticationErrorMessageResponse
    /// </summary>
    [DataContract(Name = "AuthenticationErrorMessageResponse")]
    public partial class AuthenticationErrorMessageResponse : IEquatable<AuthenticationErrorMessageResponse>, IValidatableObject {

        /// <summary>
        /// Gets or Sets Code
        /// </summary>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        [JsonPropertyName("code")]
        public AuthErrorCode? Code { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationErrorMessageResponse" /> class.
        /// </summary>
        [JsonConstructor]
        public AuthenticationErrorMessageResponse() {
            this.AdditionalProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationErrorMessageResponse" /> class.
        /// </summary>
        /// <param name="code">code.</param>
        /// <param name="message">message.</param>
        public AuthenticationErrorMessageResponse(AuthErrorCode? code = default(AuthErrorCode?), string message = default(string)) {
            this.Code = code;
            this.Message = message;
            this.AdditionalProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets or Sets Message
        /// </summary>
        [DataMember(Name = "message", EmitDefaultValue = false)]
        [JsonPropertyName("message")]
        public string? Message { get; set; }

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
        /// Builds a AuthenticationErrorMessageResponse from the JSON string presentation of the object
        /// </summary>
        /// <returns>AuthenticationErrorMessageResponse</returns>
        public static AuthenticationErrorMessageResponse FromJson(string jsonString) {
            return JsonSerializer.Deserialize<AuthenticationErrorMessageResponse>(jsonString) ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input) {
            return this.Equals(input as AuthenticationErrorMessageResponse);
        }

        /// <summary>
        /// Returns true if AuthenticationErrorMessageResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of AuthenticationErrorMessageResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AuthenticationErrorMessageResponse input) {
            if (input == null) {
                return false;
            }
            return
                (
                    this.Code == input.Code ||
                    this.Code.Equals(input.Code)
                ) &&
                (
                    this.Message == input.Message ||
                    (this.Message != null &&
                    this.Message.Equals(input.Message))
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
                hashCode = (hashCode * 9923) + this.Code.GetHashCode();
                if (this.Message != null) {
                    hashCode = (hashCode * 9923) + this.Message.GetHashCode();
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