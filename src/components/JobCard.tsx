
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { CalendarDays, MapPin, Building2 } from "lucide-react";

interface JobCardProps {
  title: string;
  company: string;
  location: string;
  date: string;
  type: string;
  onClick: () => void;
}

export function JobCard({ title, company, location, date, type, onClick }: JobCardProps) {
  return (
    <Card 
      className="hover:shadow-lg transition-all duration-300 cursor-pointer animate-fadeIn glass-card"
      onClick={onClick}
    >
      <CardHeader className="pb-2">
        <CardTitle className="text-xl font-semibold">{title}</CardTitle>
      </CardHeader>
      <CardContent>
        <div className="flex flex-col space-y-2">
          <div className="flex items-center space-x-2 text-muted-foreground">
            <Building2 className="w-4 h-4" />
            <span>{company}</span>
          </div>
          <div className="flex items-center space-x-2 text-muted-foreground">
            <MapPin className="w-4 h-4" />
            <span>{location}</span>
          </div>
          <div className="flex items-center space-x-2 text-muted-foreground">
            <CalendarDays className="w-4 h-4" />
            <span>{date}</span>
          </div>
          <div className="pt-2">
            <Badge variant="secondary">{type}</Badge>
          </div>
        </div>
      </CardContent>
    </Card>
  );
}
