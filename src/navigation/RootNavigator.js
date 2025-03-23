
import React, { useContext } from 'react';
import { View, Text, ActivityIndicator } from 'react-native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { AuthContext } from '../context/AuthContext';
import { ROUTES } from './navigationConstants';

// Import screens
import StartScreen from '../screens/StartScreen';
import LoginScreen from '../screens/LoginScreen';
import SignupScreen from '../screens/SignupScreen';
import MapScreen from '../screens/MapScreen';
import ProfileScreen from '../screens/ProfileScreen';
import SettingsScreen from '../screens/SettingsScreen';
import HistoricalPlacesScreen from '../screens/HistoricalPlacesScreen';
import AcoteScreen from '../screens/AcoteScreen';
import ReservationScreen from '../screens/ReservationScreen';

// Admin screens
import AdminDashboardScreen from '../screens/admin/AdminDashboardScreen';
import UserManagementScreen from '../screens/admin/UserManagementScreen';
import LocationManagementScreen from '../screens/admin/LocationManagementScreen';
import EventManagementScreen from '../screens/admin/EventManagementScreen';
import ReviewModerationScreen from '../screens/admin/ReviewModerationScreen';
import GlobalSupervisionScreen from '../screens/admin/GlobalSupervisionScreen';

// Provider screens
import ProviderDashboardScreen from '../screens/provider/ProviderDashboardScreen';
import AccountManagementScreen from '../screens/provider/AccountManagementScreen';
import ReservationManagementScreen from '../screens/provider/ReservationManagementScreen';
import ReviewManagementScreen from '../screens/provider/ReviewManagementScreen';
import PromotionManagementScreen from '../screens/provider/PromotionManagementScreen';

const Stack = createNativeStackNavigator();

const LoadingScreen = () => (
  <View style={{ flex: 1, justifyContent: 'center', alignItems: 'center' }}>
    <ActivityIndicator size="large" color="#0000ff" />
    <Text style={{ marginTop: 10 }}>Loading...</Text>
  </View>
);

const AuthStack = () => {
  return (
    <Stack.Navigator 
      initialRouteName={ROUTES.LOGIN}
      screenOptions={{
        headerShown: false,
      }}
    >
      <Stack.Screen name={ROUTES.START} component={StartScreen} />
      <Stack.Screen name={ROUTES.LOGIN} component={LoginScreen} />
      <Stack.Screen name={ROUTES.SIGNUP} component={SignupScreen} />
    </Stack.Navigator>
  );
};

const UserStack = () => {
  return (
    <Stack.Navigator
      initialRouteName={ROUTES.MAP}
      screenOptions={{
        headerShown: false,
      }}
    >
      {/* Map MapScreen component to both MAP and HOME routes for compatibility */}
      <Stack.Screen name={ROUTES.MAP} component={MapScreen} />
      <Stack.Screen name={ROUTES.HOME} component={MapScreen} />
      <Stack.Screen name={ROUTES.PROFILE} component={ProfileScreen} />
      <Stack.Screen name={ROUTES.SETTINGS} component={SettingsScreen} />
      <Stack.Screen name={ROUTES.HISTORICAL_PLACES} component={HistoricalPlacesScreen} />
      <Stack.Screen name={ROUTES.ACOTE} component={AcoteScreen} />
      <Stack.Screen name={ROUTES.RESERVATION} component={ReservationScreen} />
    </Stack.Navigator>
  );
};

const AdminStack = () => {
  return (
    <Stack.Navigator
      initialRouteName={ROUTES.ADMIN_DASHBOARD}
      screenOptions={{
        headerShown: false,
      }}
    >
      <Stack.Screen name={ROUTES.ADMIN_DASHBOARD} component={AdminDashboardScreen} />
      <Stack.Screen name={ROUTES.USER_MANAGEMENT} component={UserManagementScreen} />
      <Stack.Screen name={ROUTES.LOCATION_MANAGEMENT} component={LocationManagementScreen} />
      <Stack.Screen name={ROUTES.EVENT_MANAGEMENT} component={EventManagementScreen} />
      <Stack.Screen name={ROUTES.REVIEW_MODERATION} component={ReviewModerationScreen} />
      <Stack.Screen name={ROUTES.GLOBAL_SUPERVISION} component={GlobalSupervisionScreen} />
    </Stack.Navigator>
  );
};

const ProviderStack = () => {
  return (
    <Stack.Navigator
      initialRouteName={ROUTES.PROVIDER_DASHBOARD}
      screenOptions={{
        headerShown: false,
      }}
    >
      <Stack.Screen name={ROUTES.PROVIDER_DASHBOARD} component={ProviderDashboardScreen} />
      <Stack.Screen name={ROUTES.ACCOUNT_MANAGEMENT} component={AccountManagementScreen} />
      <Stack.Screen name={ROUTES.RESERVATION_MANAGEMENT} component={ReservationManagementScreen} />
      <Stack.Screen name={ROUTES.REVIEW_MANAGEMENT} component={ReviewManagementScreen} />
      <Stack.Screen name={ROUTES.PROMOTION_MANAGEMENT} component={PromotionManagementScreen} />
    </Stack.Navigator>
  );
};

const RootNavigator = () => {
  const authContext = useContext(AuthContext) || { loading: true, isAuthenticated: false, user: null };
  const { isAuthenticated, loading, user } = authContext;
  
  if (loading) {
    return <LoadingScreen />;
  }
  
  if (!isAuthenticated) {
    return <AuthStack />;
  }
  
  // Route based on user role
  if (user?.role === 'admin') {
    return <AdminStack />;
  } else if (user?.role === 'provider' || user?.role === 'prestataire') {
    return <ProviderStack />;
  } else {
    return <UserStack />;
  }
};

export default RootNavigator;
