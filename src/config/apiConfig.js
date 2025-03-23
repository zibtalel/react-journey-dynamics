
// API Configuration
export const API_URL = 'http://192.168.1.24:3000/api'; // Using the specified port 3000 and including /api path

// For Expo Go on physical devices, you might need to use your machine's local IP
// export const API_URL = 'http://192.168.1.100:3000/api';

// Development API endpoints
export const ENDPOINTS = {
  USERS: '/users',
  LOGIN: '/users/login',
  REGISTER: '/users/register', // Updated to match the required endpoint
  USER_BY_ID: (id) => `/users/${id}`,
  ALL_USERS: '/users',
  // Adding places endpoints
  PLACES: '/places',
  PLACE_BY_ID: (id) => `/places/${id}`
};

// Helper function to build full API URL
export const getApiUrl = (endpoint) => `${API_URL}${endpoint}`;
