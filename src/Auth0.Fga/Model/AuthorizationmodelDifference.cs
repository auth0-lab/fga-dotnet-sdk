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
    /// AuthorizationmodelDifference
    /// </summary>
    [DataContract(Name = "authorizationmodel.Difference")]
    public partial class AuthorizationmodelDifference : IEquatable<AuthorizationmodelDifference>, IValidatableObject {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationmodelDifference" /> class.
        /// </summary>
        /// <param name="_base">_base.</param>
        /// <param name="subtract">subtract.</param>
        public AuthorizationmodelDifference(Userset? _base = default(Userset), Userset? subtract = default(Userset)) {
            this.Base = _base;
            this.Subtract = subtract;
            this.AdditionalProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets or Sets Base
        /// </summary>
        [DataMember(Name = "base", EmitDefaultValue = false)]
        [JsonPropertyName("base")]
        public Userset Base { get; set; }

        /// <summary>
        /// Gets or Sets Subtract
        /// </summary>
        [DataMember(Name = "subtract", EmitDefaultValue = false)]
        [JsonPropertyName("subtract")]
        public Userset Subtract { get; set; }

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
        /// Builds a AuthorizationmodelDifference from the JSON string presentation of the object
        /// </summary>
        /// <returns>AuthorizationmodelDifference</returns>
        public static AuthorizationmodelDifference FromJson(string jsonString) {
            return JsonSerializer.Deserialize<AuthorizationmodelDifference>(jsonString) ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input) {
            return this.Equals(input as AuthorizationmodelDifference);
        }

        /// <summary>
        /// Returns true if AuthorizationmodelDifference instances are equal
        /// </summary>
        /// <param name="input">Instance of AuthorizationmodelDifference to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AuthorizationmodelDifference input) {
            if (input == null) {
                return false;
            }
            return
                (
                    this.Base == input.Base ||
                    (this.Base != null &&
                    this.Base.Equals(input.Base))
                ) &&
                (
                    this.Subtract == input.Subtract ||
                    (this.Subtract != null &&
                    this.Subtract.Equals(input.Subtract))
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
                if (this.Base != null) {
                    hashCode = (hashCode * 9923) + this.Base.GetHashCode();
                }
                if (this.Subtract != null) {
                    hashCode = (hashCode * 9923) + this.Subtract.GetHashCode();
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
