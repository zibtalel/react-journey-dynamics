
import { useState } from "react";
import { JobCard } from "@/components/JobCard";
import { SearchBar } from "@/components/SearchBar";
import { Dialog, DialogContent } from "@/components/ui/dialog";

const mockJobs = [
  {
    id: 1,
    title: "Senior Frontend Developer",
    company: "TechCorp",
    location: "San Francisco, CA",
    date: "Posted 2 days ago",
    type: "Full-time",
    description: "We are looking for a Senior Frontend Developer to join our team...",
  },
  {
    id: 2,
    title: "UX Designer",
    company: "DesignHub",
    location: "New York, NY",
    date: "Posted 3 days ago",
    type: "Contract",
    description: "Join our creative team as a UX Designer...",
  },
  {
    id: 3,
    title: "Backend Engineer",
    company: "CloudScale",
    location: "Remote",
    date: "Posted 1 day ago",
    type: "Full-time",
    description: "Looking for an experienced Backend Engineer...",
  },
];

export default function Index() {
  const [searchQuery, setSearchQuery] = useState("");
  const [selectedJob, setSelectedJob] = useState<typeof mockJobs[0] | null>(null);

  const filteredJobs = mockJobs.filter((job) =>
    job.title.toLowerCase().includes(searchQuery.toLowerCase()) ||
    job.company.toLowerCase().includes(searchQuery.toLowerCase())
  );

  return (
    <div className="min-h-screen bg-gradient-to-b from-gray-50 to-gray-100">
      <div className="container mx-auto py-8 px-4">
        <div className="text-center mb-12 animate-fadeIn">
          <h1 className="text-4xl font-bold mb-4">Job Descriptions</h1>
          <p className="text-muted-foreground mb-8">Find your next opportunity</p>
          <SearchBar onSearch={setSearchQuery} />
        </div>
        
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {filteredJobs.map((job) => (
            <JobCard
              key={job.id}
              title={job.title}
              company={job.company}
              location={job.location}
              date={job.date}
              type={job.type}
              onClick={() => setSelectedJob(job)}
            />
          ))}
        </div>

        <Dialog open={!!selectedJob} onOpenChange={() => setSelectedJob(null)}>
          <DialogContent className="max-w-2xl">
            {selectedJob && (
              <div className="p-4">
                <h2 className="text-2xl font-bold mb-4">{selectedJob.title}</h2>
                <div className="flex items-center gap-2 text-muted-foreground mb-4">
                  <Building2 className="w-4 h-4" />
                  <span>{selectedJob.company}</span>
                  <span>•</span>
                  <MapPin className="w-4 h-4" />
                  <span>{selectedJob.location}</span>
                </div>
                <p className="text-muted-foreground">{selectedJob.description}</p>
              </div>
            )}
          </DialogContent>
        </Dialog>
      </div>
    </div>
  );
}
