
import { useState, useEffect } from 'react';
import NetInfo, { NetInfoState } from '@react-native-community/netinfo';

export const useNetworkStatus = () => {
  const [isConnected, setIsConnected] = useState<boolean | null>(true);
  const [connectionType, setConnectionType] = useState<string | null>(null);

  useEffect(() => {
    // Initial check
    NetInfo.fetch().then(state => {
      setIsConnected(state.isConnected);
      setConnectionType(state.type);
    });

    // Subscribe to network changes
    const unsubscribe = NetInfo.addEventListener((state: NetInfoState) => {
      setIsConnected(state.isConnected);
      setConnectionType(state.type);
    });

    // Cleanup on unmount
    return () => {
      unsubscribe();
    };
  }, []);

  return { isConnected, connectionType };
};
