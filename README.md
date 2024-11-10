## GUH_Project

# Great Uni Hack Manchester - Project Base

Theme: Travelling Through Time

# Project: Epidemic Through Time

Epidemic Through Time is a virus spread simulator inspired by the theme of time travel, designed to model how an epidemic would propagate across different historical periods. This simulation emphasizes accuracy, drawing on historical data related to transportation, population density, sanitation practices, and medical knowledge to visualize pathogen spread through time.

# Overview

Epidemic Through Time lets users experience how viruses historically spread, adapted for specific eras from ancient civilizations to modern-day cities. Using AI-driven prompts, historical data, and epidemiological modeling, the simulation dynamically adjusts to each time period’s unique context. Factors like trade routes, environmental barriers, and medical practices help shape the spread of infection, visualized on an interactive globe.

# The Solution

This project provides an interactive, educational experience that highlights how virus dynamics vary over time and geography, showing how pandemics would have impacted different societies. Key components include:
	1.	Identify the Problem:
	•	Problem: How would an epidemic spread across regions and eras? By accurately modeling infection spread in historical contexts, we help users understand the roles of connectivity, geography, and public health practices in virus transmission.
	2.	Develop the Solution:
	•	Data Collection and AI Processing:
	•	City and Environment Data: For each city, the AI gathers data on population, area, trade route frequency, sanitation, and public health practices for the specified year.
	•	SEIR Model Integration: Implements the Susceptible-Exposed-Infected-Recovered (SEIR) model, adjusting infection dynamics to match each era.
	•	AI-Powered Prompting: OpenAI’s GPT model processes contextual prompts to simulate period-specific infection patterns, accounting for each historical period’s transportation modes and environmental barriers.
	•	Real-Time Epidemic Simulation:
	•	Cities become infected based on probability calculations influenced by historical trade routes and connectivity. The simulation begins with an origin city as “patient zero,” visualizing the spread across an interactive globe.
	3.	Prepare Deliverables:
	•	Frontend Interface: An intuitive globe allows users to select the origin city, year, and other parameters, visualizing infection spread based on historical factors.
	•	Comprehensive Documentation: The README explains how the model calculates infection probabilities, processes historical context with AI, and includes detailed usage instructions.
	•	Organized Codebase: Our codebase integrates data processing, AI prompts, SEIR modeling, and UI functions, structured for clarity and easy modification.

# Core Components

	1.	Historical Context Definition: Selecting an origin city and year enables the model to generate predictions based on the city’s historical connectivity, public health, and sanitation standards.
	2.	AI-Driven Prompt Engineering: GPT-4 interprets prompts that reflect the chosen period, ensuring the simulation accounts for relevant travel constraints, natural barriers, and medical practices.
	3.	Interactive Visualization: The globe interface visualizes infection spread over time, enabling users to see how virus dynamics change across different historical contexts.
	4.	Epidemiological Accuracy: By combining SEIR modeling with probability calculations, the simulation maintains realistic contagion dynamics, providing a balanced blend of historical accuracy and educational engagement.

# Creativity and Realism

This project is designed to be both accurate and engaging, making complex epidemiological and historical data accessible in a visual format. Users can explore how factors like trade routes and sanitation influenced historical epidemics, offering insights into the evolution of public health and travel systems.

Embark on a journey through history with Epidemic Through Time, and discover the ever-evolving impact of connectivity, geography, and medicine on epidemic spread across eras.
