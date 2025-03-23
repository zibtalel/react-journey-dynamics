
import React, { useState, useRef, useCallback } from 'react';
import { View, StyleSheet,  StatusBar, Text, Platform } from 'react-native';
import { COLORS } from '../theme/colors';
import { ROUTES } from '../navigation/navigationConstants';
import { SPACING } from '../theme/spacing';
import { FONT_SIZE } from '../theme/typography';
import * as Animatable from 'react-native-animatable';
import { SafeAreaView } from 'react-native-safe-area-context';
// Custom hooks
import { useLocationPermission } from '../hooks/useLocationPermission';
import { usePlacesData } from '../hooks/usePlacesData';

// Components
import MapHeader from '../components/map/MapHeader';
import CategoryFilters from '../components/map/CategoryFilters';
import MapContent from '../components/map/MapContent';
import { LoadingState, ErrorState } from '../components/map/LoadingErrorStates';
import { FooterNav } from '../components/FooterNav';

const initialRegion = {
  latitude: 36.7755,
  longitude: 8.7834,
  latitudeDelta: 0.0922,
  longitudeDelta: 0.0421,
};

const MapScreen = ({ navigation }) => {
  const mapRef = useRef(null);
  const [filterType, setFilterType] = useState(null);
  
  // Custom hooks
  const userLocation = useLocationPermission();
  const { places, isLoading, error, fetchPlaces } = usePlacesData();
  
  const handleRegionChangeComplete = (region) => {
    // Update region if needed
  };

  const toggleFilter = useCallback((type) => {
    setFilterType(filterType === type ? null : type);
  }, [filterType]);

  const filteredPlaces = places && Array.isArray(places) ? 
    (filterType ? places.filter(place => place.type === filterType) : places) 
    : [];

  if (isLoading) {
    return <LoadingState />;
  }

  if (error) {
    return <ErrorState error={error} onRetry={fetchPlaces} />;
  }

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="light-content" backgroundColor={COLORS.primary_dark} />
      
      {/* Header */}
      <Animatable.View 
        animation="fadeInDown" 
        duration={1000} 
        style={styles.header}
      >
        <Text style={styles.title}>Carte</Text>
        <Text style={styles.subtitle}>Découvrez les lieux touristiques</Text>
      </Animatable.View>
      
      <Animatable.View 
        animation="fadeInUp"
        duration={800}
        style={styles.mainContent}
      >
        {/* Category Filters */}
        <CategoryFilters 
          filterType={filterType}
          toggleFilter={toggleFilter}
        />
        
        {/* Map */}
        <View style={styles.mapContainer}>
          <MapContent 
            mapRef={mapRef}
            initialRegion={initialRegion}
            userLocation={userLocation}
            filteredPlaces={filteredPlaces}
            onRegionChangeComplete={handleRegionChangeComplete}
          />
        </View>
      </Animatable.View>
      
      {/* Footer - Updated to set HOME as active route */}
      <FooterNav navigation={navigation} activeScreen={ROUTES.HOME} />
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: COLORS.white,
  },
  header: {
    backgroundColor: COLORS.primary,
    padding: SPACING.lg,
    paddingTop: Platform.OS === 'android' ? SPACING.xl : SPACING.lg,
    borderBottomLeftRadius: 20,
    borderBottomRightRadius: 20,
  },
  title: {
    fontSize: FONT_SIZE.xl,
    fontWeight: 'bold',
    color: COLORS.white,
  },
  subtitle: {
    fontSize: FONT_SIZE.md,
    color: COLORS.white,
    opacity: 0.8,
    marginTop: SPACING.xs,
  },
  mainContent: {
    flex: 1,
    padding: SPACING.sm,
  },
  mapContainer: {
    flex: 1,
    borderRadius: 15,
    overflow: 'hidden',
    marginTop: SPACING.sm,
    ...Platform.select({
      ios: {
        shadowColor: COLORS.black,
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.2,
        shadowRadius: 4,
      },
      android: {
        elevation: 4,
      },
    }),
  },
});

export default MapScreen;
