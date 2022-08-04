How long did this assignment take?
> I took about 5 hours 30 mins. Not including breaks and not including writting the QnA and the Instructions sections.

What was the hardest part?
> Figuring out how to restrict the PUT request to the same user.

Did you learn anything new?
> Absolutely, I honestly had never implemented JWT authentication from scratch. I learned many things from getting up to speed with the standard, understanding how each section of the structure works, use cases, to common pitfalls.

Is there anything you would have liked to implement but didn't have the time to?
> I have so many ideas to make this API better! To name a few:
> 1. I could not use the `x-authentication-token` header because .Net has a strong opinion on using Token Bearer auth instead. If I wanted to use a custom header, I could have implemented a middleware in the pipeline. However, I'm sure that would have put me over 6 hours time on the solution (hopefully, this is not a deal breaker).
2. I added the credentials in the appsettings, so at least they aren't hard coded, but I would love to use a more secure service like [Azure Key Vault](https://azure.microsoft.com/en-us/services/key-vault/)
3. Even though tokens expire, I feel I could have used other Claims better. Maybe implement Role based auth
4. Middleware for exception handling and enforcing a standard such as [JSON Specification](https://jsonapi.org/format/).
5. Would love to add more unit tests in the future. Maybe if there was more business logic involved.
6. Add Bearer Token auth to the swagger page for easy testing.

What are the security holes (if any) in your system? If there are any, how would you fix them?
> 1. Storing secrets like connection strings and JWT secrets in the appsettings (or anywhere part of the repo) is very unsecure. If someone really knows what they are doing, they can gain access. To fix this, I would use a more secure service like [Azure Key Vault.](https://azure.microsoft.com/en-us/services/key-vault/) At the very least, I would let the CI CD pipeline inject the secrets as environment variables or in an appsettings template.
2. I also don't have any unique constraints in the password plus, storing plain text passwords in a database is not very secure. I would love to use a Hash and Salt based encryption instead.
3. While the database enforces email uniqueness, I would love to add validations at the API/Services level.

Do you feel that your skills were well tested?
> Yes, this test was both fun and challenging. Looking forward to the next steps! 😊
