### 1) Story about Microservices and Distributed Monolith and Its Problems
- **Introduction to Microservices:**
  - Explain the microservices architecture and its benefits (e.g., scalability, flexibility).
  - Contrast with monolithic architectures.
- **Challenges in Microservices:**
  - Distributed nature leading to complexity in communication.
  - Dependency management between services.
  - Latency and network issues.
  - Example: Microservice A depends on Microservice B and C, both of which can fail independently, causing cascading failures.

### 2) Additional Problems with Third-Party Services that Polly Can Help Resolve
- **Issues with Third-Party Services:**
  - Unreliable third-party APIs causing downtime.
  - Rate limiting and throttling by third-party services.
  - Latency issues and intermittent failures.
- **Examples:**
  - Payment gateways going down.
  - External data providers experiencing downtime or slow responses.

### 3) Improving Resiliency and Fixing Problems Without Polly
- **Basic Strategies Without Polly:**
  - Implementing retry mechanisms manually.
  - Circuit breakers and fallbacks.
  - Bulkhead isolation.
  - Use of health checks and orchestrators to manage service states.
- **Examples:**
  - Custom retry logic using loops and error handling.
  - Simple circuit breaker implementation using flags or counters.

### 4) Polly Resilience Strategies: Proactive vs. Reactive Approach
- **Overview of Polly:**
  - Introduction to Polly library and its purpose.
- **Reactive Strategies:**
  - **Retry:** Automatically retry a failed operation.
  - **Circuit Breaker:** Stop calling a failing service for a period.
  - **Fallback:** Provide an alternative value or service when the main service fails.
- **Proactive Strategies:**
  - **Bulkhead Isolation:** Limit the number of concurrent calls to a service.
  - **Timeouts:** Limit the time a call can take.
  - **Cache:** Cache responses to reduce load and improve speed.
- **Difference Between Proactive and Reactive:**
  - Proactive focuses on preventing failures before they occur.
  - Reactive handles failures after they occur.

### 5) Getting Started in ASP.NET Core App
- **Setting Up Polly:**
  - Installing Polly via NuGet.
  - Basic configuration and setup in an ASP.NET Core app.
- **Example:**
  - Adding a retry policy to an HTTP client.

```csharp
services.AddHttpClient("ExampleClient")
    .AddPolicyHandler(Policy.Handle<HttpRequestException>()
    .RetryAsync(3));
```

### 6) Mixing Different Strategies and the Rules Behind It
- **Combining Policies:**
  - How to use multiple Polly policies together (e.g., Retry + Circuit Breaker).
  - Importance of order and execution.
- **Resilience Context:**
  - Using Polly’s Context to pass data through policies.
  - Examples of using Context for logging or metrics.

### 7) Testing Pipelines (Introduction to Chaos Engineering)
- **Importance of Testing Resilience:**
  - Why we need to test our resilience strategies.
- **Chaos Engineering:**
  - Principles of chaos engineering.
  - Tools and techniques (e.g., Chaos Monkey, ToxiProxy).
- **Example:**
  - Simulating a service failure and observing Polly’s response.

### 8) Examples of Extensibility
- **Custom Policies:**
  - Creating custom Polly policies for specific needs.
- **Extending Existing Policies:**
  - Adding logging, metrics, or other functionality to existing Polly policies.
- **Example:**
  - Custom policy for rate limiting.

```csharp
var rateLimitPolicy = Policy.BulkheadAsync<HttpResponseMessage>(maxParallelization: 2, maxQueuingActions: 4);
```

### Conclusion
- **Summary:**
  - Recap of key points covered in the lecture.
- **Q&A:**
  - Open the floor for questions and discussion.