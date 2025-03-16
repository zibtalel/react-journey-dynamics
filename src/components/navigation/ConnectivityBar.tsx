
import React from 'react';
import { View, Text, StyleSheet, Platform, StatusBar } from 'react-native';
import { Wifi, WifiOff } from 'lucide-react-native';
import { useNetworkStatus } from '../../hooks/useNetworkStatus';
import { useThemeColors } from '../../hooks/useThemeColors';
import { wp, hp } from '../../utils/responsive';

const STATUSBAR_HEIGHT = Platform.OS === 'ios' ? 50 : StatusBar.currentHeight || 24;

const ConnectivityBar = () => {
  const { isConnected } = useNetworkStatus();
  const colors = useThemeColors();

  return (
    <View style={[
      styles.container, 
      { 
        backgroundColor: isConnected 
          ? colors.isDark ? 'rgba(30, 41, 59, 0.8)' : 'rgba(248, 250, 252, 0.8)' 
          : colors.isDark ? 'rgba(239, 68, 68, 0.2)' : 'rgba(239, 68, 68, 0.1)',
        borderBottomColor: colors.border,
        marginTop: STATUSBAR_HEIGHT,
      }
    ]}>
      <View style={styles.statusContainer}>
        {isConnected ? (
          <>
            <Wifi size={wp(12)} color={colors.success} strokeWidth={2} />
            <Text style={[styles.statusText, { color: colors.success }]}>
              Vous êtes en ligne
            </Text>
          </>
        ) : (
          <>
            <WifiOff size={wp(12)} color={colors.error} strokeWidth={2} />
            <Text style={[styles.statusText, { color: colors.error }]}>
              Vous êtes hors ligne
            </Text>
          </>
        )}
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    paddingVertical: hp(4),
    paddingHorizontal: wp(15),
    borderBottomWidth: 0.5,
    flexDirection: 'row',
    justifyContent: 'flex-end',
    position: 'absolute',
    top: 0,
    left: 0,
    right: 0,
    zIndex: 10,
    backdropFilter: 'blur(8px)',
  },
  statusContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  statusText: {
    fontSize: wp(16),
    fontWeight: '600',
    marginLeft: wp(4),
  },
});

export default ConnectivityBar;
